using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;

namespace ApplicationServices.Commands
{
    public class RegisterNewParkingSpaceCommand : IRequest<bool>
    {
        public RegisterNewParkingSpaceCommand(
            string ownerId,
            ParkingSpaceDescription description,
            Address address,
            SpaceAvailability availability,
            BookingRate bookingRate)
        {
            OwnerId = ownerId;
            Description = description;
            Address = address;
            Availability = availability;
            BookingRate = bookingRate;
        }

        public string OwnerId { get; }
        public ParkingSpaceDescription Description { get; }
        public Address Address { get; }
        public SpaceAvailability Availability { get; }
        public BookingRate BookingRate { get; }
    }
}