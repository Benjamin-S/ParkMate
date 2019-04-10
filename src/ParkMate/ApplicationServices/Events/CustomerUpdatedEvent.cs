using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class CustomerUpdatedEvent : INotification
    {
        public CustomerUpdatedEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; }
    }
}
