using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public DeleteVehicleCommandHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            vehicle.SetAsDeleted();

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
