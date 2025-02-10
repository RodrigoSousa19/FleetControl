using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.EnableVehicle
{
    public class EnableVehicleHandler : IRequestHandler<EnableVehicleCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnableVehicleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Handle(EnableVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            if (vehicle.Enabled)
                return ResultViewModel.Error("O veículo já se encontra ativo.");

            vehicle.Enable();

            await _unitOfWork.VehicleRepository.Update(vehicle);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
