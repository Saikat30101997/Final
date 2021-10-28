using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web.Areas.Admin.Models.Customers
{
    public class CustomerListModel
    {
        private readonly ICustomerService _customerService;
        public CustomerListModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public CustomerListModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        internal object GetCustomers(DataTablesAjaxRequestModel tableModel)
        {
            var data = _customerService.GetCustomers(
              tableModel.PageIndex,
              tableModel.PageSize,
              tableModel.SearchText,
              tableModel.GetSortText(new string[] { "Name", "Age", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Age.ToString(),
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void DeleteCustomer(int id)
        {
            _customerService.Delete(id);
        }
    }
}
