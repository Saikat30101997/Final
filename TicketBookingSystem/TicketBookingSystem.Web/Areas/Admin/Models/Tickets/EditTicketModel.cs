using AutoMapper;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Web.Areas.Admin.Models.Tickets
{
    public class EditTicketModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        internal void GetTicket(int id)
        {
            var ticket = _ticketService.GetTicket(id);
            _mapper.Map(ticket, this);
        }

        internal void Update()
        {
            var ticket = _mapper.Map<Ticket>(this);
            _ticketService.Update(ticket);
        }
    }
}
