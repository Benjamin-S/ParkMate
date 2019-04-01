using System.Collections.Generic;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;

namespace ApplicationServices.Commands
{
    public class EditParkingSpaceAddressCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceAddressCommand(int parkingSpaceId, Address address)
        {
            ParkingSpaceId = parkingSpaceId;
            Address = address;
        }
        public int ParkingSpaceId { get; }
        public Address Address { get; }
    }
}
