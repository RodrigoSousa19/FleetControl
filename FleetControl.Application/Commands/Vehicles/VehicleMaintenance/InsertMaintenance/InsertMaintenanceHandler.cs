using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class InsertMaintenanceHandler : IRequestHandler<InsertMaintenanceCommand, ResultViewModel<VehicleMaintenance>>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public InsertMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<VehicleMaintenance>> Handle(InsertMaintenanceCommand request, CancellationToken cancellationToken)
        {

            new Validator()
                .IsGreaterThanZero(request.TotalCost, ErrorsList.InvalidTotalCostValue)
                .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            var maintenance = await _repository.Create(request.ToEntity());

            return ResultViewModel<VehicleMaintenance>.Success(maintenance);
        }
    }
}
