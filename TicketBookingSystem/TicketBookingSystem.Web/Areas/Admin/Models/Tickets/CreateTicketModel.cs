using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Web.Areas.Admin.Models.Tickets
{
    public class CreateTicketModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public CreateTicketModel(ITicketService ticketService,IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        internal void Create()
        {
            var ticket = _mapper.Map<Ticket>(this);
            _ticketService.CreateTicket(ticket);
        }
    }
}
