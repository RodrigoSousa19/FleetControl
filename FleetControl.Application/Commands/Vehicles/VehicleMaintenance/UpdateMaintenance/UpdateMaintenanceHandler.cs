using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class UpdateMaintenanceHandler : IRequestHandler<UpdateMaintenanceCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            new Validator()
            .IsGreaterThanZero(request.TotalCost, ErrorsList.InvalidTotalCostValue)
            .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
            .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
            .Validate();

            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.GetById(request.Id);

            if (maintenance is not null)
            {
                maintenance.Update(request.Description, request.TotalCost, request.StartDate, request.EndDate);

                await _unitOfWork.VehicleMaintenanceRepository.Update(maintenance);

                await _unitOfWork.SaveChangesAsync();

                return ResultViewModel.Success();
            }

            return ResultViewModel.Error("Não foi possível localizar a manutenção especificada");
        }
    }
}
