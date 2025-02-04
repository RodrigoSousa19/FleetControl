using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.InsertVehicle
{
    public class InsertVehicleHandler : IRequestHandler<InsertVehicleCommand, ResultViewModel<Vehicle>>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public InsertVehicleHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Vehicle>> Handle(InsertVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.Create(request.ToEntity());

            return ResultViewModel<Vehicle>.Success(vehicle);
        }
    }
}
