using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.DeleteVehicle
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            vehicle.SetAsDeleted();

            await _unitOfWork.VehicleRepository.Update(vehicle);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
