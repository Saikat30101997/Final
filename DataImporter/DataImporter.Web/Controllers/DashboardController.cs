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
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(ILogger<DashboardController> logger,
            UserManager<ApplicationUser>userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            
            return View();
        }
       
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(CreateImportModel model)
        {
            try
            {
                model.Create();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to create Product");
                _logger.LogError(ex, "Product Creation Failed");
            }
            return View(model);
        }

        public JsonResult GroupName()
        {
            var contactModel = new ContactModel();
            return Json(contactModel.GroupList);
        }

       
        public IActionResult Contacts()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            return View();
        }
        public IActionResult ImportContact()
        {
            return View();
        }
        public IActionResult ImportJob()
        {
            return View();
        }
        public IActionResult ExportJob()
        {
            return View();
        }
        public IActionResult Register1()
        {
            return View();
        }
    }
}
