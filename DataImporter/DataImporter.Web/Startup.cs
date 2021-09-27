using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataImporter.Common;
using DataImporter.Common.Utilities;
using DataImporter.Importer;
using DataImporter.Importer.Contexts;
using DataImporter.Membership;
using DataImporter.Membership.BusinessObjects;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Entities;
using DataImporter.Membership.Services;
using DataImporter.Web.Models.ReCaptcha;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DataImporter.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            WebHostEnvironment = env;

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public  ILifetimeScope AutofacContainer { get; set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();
            builder.RegisterModule(new MembershipModule(connectionInfo.connectionString,
                 connectionInfo.migrationAssemblyName));
            builder.RegisterModule(new ImporterModule(connectionInfo.connectionString,
              connectionInfo.migrationAssemblyName));
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new WebModule());
        }
        private (string connectionString, string migrationAssemblyName) GetConnectionStringAndAssemblyName()
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            return (connectionString, migrationAssemblyName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionInfo.connectionString, b =>
                b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));
            services.AddDbContext<ImporterDbContext>(options =>
              options.UseSqlServer(connectionInfo.connectionString, b =>
              b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.Configure<ReCaptchaSettings>(Configuration.GetSection("GooglereCaptcha"));
            services.Configure<ConfirmationEmailSettings>(Configuration.GetSection("ConfirmEmail"));
            services.Configure<SmtpConfiguration>(Configuration.GetSection("Smtp"));
            services.Configure<FilePath>(Configuration.GetSection("Paths"));
            services  
                 .AddIdentity<ApplicationUser, Role>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddUserManager<UserManager>() 
                 .AddRoleManager<RoleManager>()
                 .AddSignInManager<SignInManager>()
                 .AddDefaultUI()
                 .AddDefaultTokenProviders();

    

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(options => 
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.AddAuthorization(options=> {
                options.AddPolicy("AccessPermission", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new AccessRequirement());
                });
            });
            services.AddSingleton<IAuthorizationHandler, AccessRequirementHandler>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
