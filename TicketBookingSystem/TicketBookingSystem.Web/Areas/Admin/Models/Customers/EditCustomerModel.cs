using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Web.Areas.Admin.Models.Customers
{
    public class EditCustomerModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Address { get; set; }
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public EditCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        internal void GetCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            _mapper.Map(customer, this);
        }

        internal void Update()
        {
            var customer = _mapper.Map<Customer>(this);
            _customerService.Update(customer);
        }
    }
}
