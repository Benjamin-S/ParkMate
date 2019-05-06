using System;
using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceRegisteredEvent : INotification
    {
        public ParkingSpaceRegisteredEvent(ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }
}
