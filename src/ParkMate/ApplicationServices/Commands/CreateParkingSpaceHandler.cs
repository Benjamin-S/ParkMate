using System;
using System.Threading.Tasks;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;

namespace ApplicationServices.Commands
{
    public class CreateParkingSpaceHandler 
        : ICommandHandler<CreateParkingSpace>
    {
        private IRepository<BaseEntity> _repository;

        public CreateParkingSpaceHandler(IRepository<BaseEntity> repository)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }
        public async Task<bool> Handle(CreateParkingSpace command)
        {
            var parkingSpace = new ParkingSpace(command.OwnerId, command.Description, 
                command.Address, command.Availability, command.BookingRate);
            
            await _repository.AddAsync(parkingSpace);
            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}