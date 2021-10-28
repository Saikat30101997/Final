using AutoMapper;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Web.Areas.Admin.Models.Customers
{
    public class CreateCustomerModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Address { get; set; }
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CreateCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        internal void Create()
        {
            var customer = _mapper.Map<Customer>(this);
            _customerService.CreateCustomer(customer);
        }
    }
}
