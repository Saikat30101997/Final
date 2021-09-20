using Autofac;
using DataImporter.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<EmailService>().As<IEmailService>()
            .WithParameter("host", "smtp.gmail.com")
            .WithParameter("port", 465)
            .WithParameter("username", "saikat.cse1997@gmail.com")
            .WithParameter("password", "hello@saikat")
            .WithParameter("useSSL", true)
            .WithParameter("from", "saikat.cse1997@gmail.com")
            .InstancePerLifetimeScope();
            builder.RegisterType<FileSearching>().As<IFileSearching>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
