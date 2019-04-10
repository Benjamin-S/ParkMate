using System;
using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class NewCustomerRegisteredEvent : INotification
    {
        public NewCustomerRegisteredEvent(Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; }
    }
}
