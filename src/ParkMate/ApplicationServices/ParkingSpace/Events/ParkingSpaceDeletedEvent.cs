using System;
using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceDeletedEvent : INotification
    {
        public ParkingSpaceDeletedEvent(ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }
}
