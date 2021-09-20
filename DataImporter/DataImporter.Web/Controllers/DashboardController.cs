using DataImporter.Membership.Entities;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Controllers
{
    [Authorize(Policy = "AccessPermission")]
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

        public IActionResult ManageGroup()
        {
            var model = new GroupListModel();
            return View(model);
        }

        public JsonResult GetGroupData()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new GroupListModel();
            var data = model.GetGroups(tableModel,Id);
            return Json(data);
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
                ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
                string s = ViewBag.UserId;
                Guid Id = Guid.Parse(s);
                model.Create(Id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to create Group");
                _logger.LogError(ex, "Group Creation Failed");
            }
            return View(model);
        }

        public JsonResult GroupName()
        {
            var contactModel = new ContactModel();
            var data = contactModel.GetGroups();
            return Json(data);
        }

       
        public IActionResult Contacts()
        {
            var contactModel = new ContactModel();
            var data = contactModel.GetGroups();
            ViewBag.Groups = new SelectList(data, "Value", "Text");
            return View();
        }
        public IActionResult ImportContact()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult ImportContact(ImportContactModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
                    string s = ViewBag.UserId;
                    Guid Id = Guid.Parse(s);
                    model.Create(model.Id,Id);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create Excel");
                    _logger.LogError(ex, "Excel Creation Failed");
                }
            }
            //  return View(model);
            return RedirectToAction(nameof(UploadConfirmation));
        }
        public IActionResult GroupEdit(int id)
        {
            var model = new EditGroupModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult GroupEdit(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to Edit Group");
                    _logger.LogError(ex, "Group Creation Failed");
                }
            }
            return View(model);
        }
        public IActionResult UploadConfirmation()
        {
            var model = new ImportContactModel();
            model.ExcelValues();
            return View(model);
        }
      
        public IActionResult UploadConfirm()
        {
            var model = new ImportContactModel();
            model.Upload();
            return RedirectToAction(nameof(ImportJob));
        }

        public IActionResult DeleteFile()
        {
            var model = new ImportContactModel();
            model.Delete();
            return RedirectToAction(nameof(ImportContact));
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
