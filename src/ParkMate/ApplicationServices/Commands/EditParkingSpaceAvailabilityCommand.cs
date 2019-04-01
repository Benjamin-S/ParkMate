using System.Collections.Generic;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;

namespace ApplicationServices.Commands
{
    public class EditParkingSpaceAvailabilityCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceAvailabilityCommand(int parkingSpaceId, List<AvailabilityTime> times)
        {
            ParkingSpaceId = parkingSpaceId;
            AvailabilityTimes = times;
        }
        public int ParkingSpaceId { get; }
        public IReadOnlyList<AvailabilityTime> AvailabilityTimes { get; }
    
    }
}