using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.UnitOfWorks;

namespace TicketBookingSystem.Booking.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;
        private readonly IMapper _mapper;
        public TicketService(IBookingUnitOfWork bookingUnitOfWork,IMapper mapper)
        {
            _bookingUnitOfWork = bookingUnitOfWork;
            _mapper = mapper;
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is not provided");
            _bookingUnitOfWork.Tickets.Add(
                _mapper.Map<Entities.Ticket>(ticket));
            _bookingUnitOfWork.Save();
        }

        public void DeleteTicket(int id)
        {
            _bookingUnitOfWork.Tickets.Remove(id);
            _bookingUnitOfWork.Save();
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _bookingUnitOfWork.Tickets.GetById(id);
            return _mapper.Map<Ticket>(ticket);
        }

        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            var ticketData = _bookingUnitOfWork.Tickets.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.CustomerId.ToString().Contains(searchText), sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketData.data
                              select _mapper.Map<Ticket>(ticket)).ToList();

            return (resultData, ticketData.total, ticketData.totalDisplay);
        }

        public void Update(Ticket ticket )
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is not provided");
            var ticketEntity = _bookingUnitOfWork.Tickets.GetById(ticket.Id);
            if (ticketEntity != null)
            {
                _mapper.Map(ticket, ticketEntity);
                _bookingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Ticket is not edited");
        }
    }
}
