using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceUpdatedEvent : INotification
    {
        public ParkingSpaceUpdatedEvent(ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }
}
