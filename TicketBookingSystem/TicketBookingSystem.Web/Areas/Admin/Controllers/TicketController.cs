using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Web.Areas.Admin.Models.Tickets;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.SomeData = "All Available Tickets";
            var model = new TicketListModel();
            return View(model);
        }

        public JsonResult GetTicketData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new TicketListModel();
            var data = model.GetTickets(tableModel);
            return Json(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create(CreateTicketModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.Create();

                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create Ticket");
                    _logger.LogError(ex, "Ticket creation failed");
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = new EditTicketModel();
            model.GetTicket(id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to edit Ticket");
                    _logger.LogError(ex, "Ticket edition failed");
                }
            }
            return View(model);
            
        }

        public ActionResult Delete(int id)
        {
            var model = new TicketListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
