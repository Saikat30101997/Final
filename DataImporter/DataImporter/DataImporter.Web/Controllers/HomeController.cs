using Autofac;
using DataImporter.Membership.Entities;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Controllers
{
    [Authorize(Policy = "AccessPermission")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;
        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager, ILifetimeScope scope)
        {
            _logger = logger;
            _userManager = userManager;
            _scope = scope;
        }
        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            var model = _scope.Resolve<ListModel>();
            model.GetData(Id);
            return View(model);
        }
    }
}
