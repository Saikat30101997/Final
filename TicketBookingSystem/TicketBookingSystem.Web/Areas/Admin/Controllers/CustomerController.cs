using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Web.Areas.Admin.Models.Customers;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.SomeData = "All Avaialble Customers";
            var model = new CustomerListModel();
            return View(model);
        }

        public JsonResult GetCustomerData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new CustomerListModel();
            var data = model.GetCustomers(tableModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(CreateCustomerModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.Create();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create Customer");
                    _logger.LogError(ex, "Customer Creation Failed");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditCustomerModel();
            model.GetCustomer(id);
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Edit(EditCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to Edit Customer");
                    _logger.LogError(ex, "Customer Edition Failed");
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = new CustomerListModel();
            model.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
    
}
