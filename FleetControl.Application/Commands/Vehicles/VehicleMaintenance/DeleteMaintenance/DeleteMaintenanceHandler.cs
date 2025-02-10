using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class DeleteMaintenanceHandler : IRequestHandler<DeleteMaintenanceCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.GetById(request.Id);

            if (maintenance is not null)
            {
                maintenance.SetAsDeleted();

                await _unitOfWork.VehicleMaintenanceRepository.Update(maintenance);

                await _unitOfWork.SaveChangesAsync();

                return ResultViewModel.Success();
            }

            return ResultViewModel.Error("Não foi possível localizar a manutenção especificada");
        }
    }
}
