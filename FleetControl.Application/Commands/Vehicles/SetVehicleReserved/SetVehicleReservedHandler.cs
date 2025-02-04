using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleReserved
{
    public class SetVehicleReservedHandler : IRequestHandler<SetVehicleReservedCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public SetVehicleReservedHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(SetVehicleReservedCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.IdVehicle);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veiculo especificado");

            if (vehicle.Status != VehicleStatus.Available)
                return ResultViewModel.Error("O status atual do veículo não permite que ele seja reservado");

            vehicle.SetReserved(request.IdProject);

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
