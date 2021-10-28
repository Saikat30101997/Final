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
    public class CustomerService : ICustomerService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;
        private readonly IMapper _mapper;
        public CustomerService(IBookingUnitOfWork bookingUnitOfWork, IMapper mapper)
        {
            _bookingUnitOfWork = bookingUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidOperationException("Customer is not provided");
            _bookingUnitOfWork.Customers.Add(
                _mapper.Map<Entities.Customer>(customer));
            _bookingUnitOfWork.Save();
        }

        public void Delete(int id)
        {
            _bookingUnitOfWork.Customers.Remove(id);
            _bookingUnitOfWork.Save();
        }

        public Customer GetCustomer(int id)
        {
            var customer = _bookingUnitOfWork.Customers.GetById(id);
            return _mapper.Map<Customer>(customer);
        }

        public (IList<Customer> records, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize, 
            string searchText, string sortText)
        {
            var customerData = _bookingUnitOfWork.Customers.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultdata = (from customer in customerData.data
                              select new Customer
                              {
                                  Name = customer.Name,
                                  Age = customer.Age,
                                  Address = customer.Address,
                                  Id = customer.Id
                              }).ToList();

            return (resultdata, customerData.total, customerData.totalDisplay);
        }

        public void Update(Customer customer)
        {

            if (customer == null)
                throw new InvalidOperationException("Customer is not provided");

            var customerEntity = _bookingUnitOfWork.Customers.GetById(customer.Id);
            if (customerEntity != null)
            {
                _mapper.Map(customer, customerEntity);
                _bookingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Could not find Customer");
        }
    }
}
