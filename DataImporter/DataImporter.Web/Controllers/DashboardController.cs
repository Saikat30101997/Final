using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Membership.Entities;
using DataImporter.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Controllers
{
    [Authorize(Policy = "AccessPermission")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;
        public DashboardController(ILogger<DashboardController> logger,
            UserManager<ApplicationUser>userManager,ILifetimeScope scope)
        {
            _logger = logger;
            _userManager = userManager;
            _scope = scope;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult ManageGroup()
        {
            var model = _scope.Resolve<GroupListModel>();
            return View(model);
        }

        public JsonResult GetGroupData()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<GroupListModel>();
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
                model.Resolve(_scope);
                model.Create(Id);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to create Group");
                _logger.LogError(ex, "Group Creation Failed");
            }
            return View(model);
        }

        public IActionResult GroupDelete(int id)
        {
            var model = _scope.Resolve<GroupListModel>();
            model.Delete(id);
            return RedirectToAction(nameof(ManageGroup));
        }

       
        public IActionResult Contacts()
        {
            var contactModel = _scope.Resolve<ContactModel>();
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            contactModel.GetGroups(Id);
            return View(contactModel);
        }
        [HttpPost]
        public IActionResult Contacts(ContactModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
                    string s = ViewBag.UserId;
                    Guid Id = Guid.Parse(s);
                    model.Resolve(_scope);
                    model.GetGroups(Id);
                    model.GetContactsData(Id,model.GroupName, model.DateFrom.Value, model.DateTo.Value);

                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed");
                    _logger.LogError(ex, "Data is not get");

                }
            }
            return View(model);
        }
        public IActionResult ImportContact()
        {
            var model = _scope.Resolve<ImportContactModel>();
            return View(model);
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
                    model.Resolve(_scope);
                    model.Create(model.Id,Id);
                   
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create Excel");
                    _logger.LogError(ex, "Excel Creation Failed");
                }
            }
            if (model.fileMatchValue == 1) return View(model);
            if (model.ExcelFile == null) return View(model);
            else
               return RedirectToAction(nameof(UploadConfirmation));
        }
        public IActionResult GroupEdit(int id)
        {
            var model = _scope.Resolve<EditGroupModel>();
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
                    model.Resolve(_scope);
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
            var model = _scope.Resolve<ImportContactModel>();
            model.ExcelValues();
            return View(model);
        }
      
        public IActionResult UploadConfirm()
        {
            var model = _scope.Resolve<ImportContactModel>();
            model.Upload();
            return RedirectToAction(nameof(ImportJob));
        }

        public IActionResult DeleteFile()
        {
            var model = _scope.Resolve<ImportContactModel>();
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            model.Delete(Id,0,string.Empty);
            return RedirectToAction(nameof(ImportContact));
        }
        public IActionResult ImportJob()
        {
            var model = _scope.Resolve<ImportJobModel>();
            return View(model);
        }

        public JsonResult GetImportData()
        {
            
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ImportJobModel>();
            var data = model.GetImports(tableModel, Id);
            return Json(data);
        }

        public async Task<ActionResult> Export(int id)
        {
            var usr =await _userManager.GetUserAsync(User);
            var email = usr?.Email;
            var model = _scope.Resolve<ExportJobModel>();
            model.LoadModelData(id,email);
            return RedirectToAction(nameof(ConfirmExport));
        }
        public IActionResult ExportJob()
        {
            var model = _scope.Resolve<ExportJobModel>();
            return View(model);
        }

        public JsonResult GetExportData()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid Id = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ExportJobModel>();
            var data = model.GetExports(tableModel, Id);
            return Json(data);
        }


        public async Task<ActionResult> SendEmail(int id)
        {
            var usr = await _userManager.GetUserAsync(User);
            var email = usr?.Email;
            var model = _scope.Resolve<ExportJobModel>();
            model.Send(id, email);
            return RedirectToAction(nameof(ExportJob));
        }

        public FileStreamResult Download(int id)
        {
            var model = _scope.Resolve<ExportJobModel>();
            var data = model.Download(id);
            return File(data.fileStream, data.mimetype, data.file);
        }

        public IActionResult DeleteImport(int id)
        {
            var model = _scope.Resolve<ImportJobModel>();
            model.Delete(id);
            return RedirectToAction(nameof(ImportJob));
        }

        public IActionResult ConfirmExport()
        {
            return View();
        }

    
    }
}
