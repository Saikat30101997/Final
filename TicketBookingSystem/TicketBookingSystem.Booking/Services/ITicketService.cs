using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Services
{
    public interface ITicketService
    {
        (IList<Ticket>records,int total,int totalDisplay) GetTickets(int pageIndex, 
            int pageSize, string searchText, string sortText);
        void CreateTicket(Ticket ticket);
        Ticket GetTicket(int id);
        void Update(Ticket ticket);
        void DeleteTicket(int id);
    }
}
