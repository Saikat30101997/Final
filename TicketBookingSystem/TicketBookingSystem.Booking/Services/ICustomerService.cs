using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Services
{
    public interface ICustomerService
    {
        (IList<Customer>records,int total,int totalDisplay) GetCustomers(int pageIndex, int pageSize, 
            string searchText, string sortText);
        void CreateCustomer(Customer customer);
        Customer GetCustomer(int id);
        void Update(Customer customer);
        void Delete(int id);
    }
}
