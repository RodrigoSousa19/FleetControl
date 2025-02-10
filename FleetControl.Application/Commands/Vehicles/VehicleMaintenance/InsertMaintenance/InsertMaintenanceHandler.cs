using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class InsertMaintenanceHandler : IRequestHandler<InsertMaintenanceCommand, ResultViewModel<VehicleMaintenance>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<VehicleMaintenance>> Handle(InsertMaintenanceCommand request, CancellationToken cancellationToken)
        {

            new Validator()
                .IsGreaterThanZero(request.TotalCost, ErrorsList.InvalidTotalCostValue)
                .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<VehicleMaintenance>.Success(maintenance);
        }
    }
}
