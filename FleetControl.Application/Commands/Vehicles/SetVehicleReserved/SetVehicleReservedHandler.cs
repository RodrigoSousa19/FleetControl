using FleetControl.Application.Models;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleReserved
{
    public class SetVehicleReservedHandler : IRequestHandler<SetVehicleReservedCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public SetVehicleReservedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(SetVehicleReservedCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.IdVehicle);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veiculo especificado");

            if (vehicle.Status != VehicleStatus.Available)
                return ResultViewModel.Error("O status atual do veículo não permite que ele seja reservado");

            vehicle.SetReserved(request.IdProject);

            await _unitOfWork.VehicleRepository.Update(vehicle);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
