using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Web.Areas.Admin.Models.Customers;
using TicketBookingSystem.Web.Areas.Admin.Models.Tickets;

namespace TicketBookingSystem.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCustomerModel, Customer>().ReverseMap();
            CreateMap<EditCustomerModel, Customer>().ReverseMap();
            CreateMap<CreateTicketModel, Ticket>().ReverseMap();
            CreateMap<EditTicketModel, Ticket>().ReverseMap();
        }
    }
}
