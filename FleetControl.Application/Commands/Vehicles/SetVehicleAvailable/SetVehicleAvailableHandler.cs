using FleetControl.Application.Models;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleAvailable
{
    public class SetVehicleAvailableHandler : IRequestHandler<SetVehicleAvailableCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public SetVehicleAvailableHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(SetVehicleAvailableCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veiculo especificado");

            if (vehicle.Status == VehicleStatus.Available)
                return ResultViewModel.Error("O veículo já se encontra ativo e disponível.");

            vehicle.SetAvailable();

            await _unitOfWork.VehicleRepository.Update(vehicle);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
