using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class UpdateMaintenanceHandler : IRequestHandler<UpdateMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public UpdateMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            new Validator()
            .IsGreaterThanZero(request.TotalCost, ErrorsList.InvalidTotalCostValue)
            .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
            .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
            .Validate();

            var maintenance = await _repository.GetById(request.Id);

            if (maintenance is not null)
            {
                maintenance.Update(request.Description, request.TotalCost, request.StartDate, request.EndDate);

                await _repository.Update(maintenance);

                return ResultViewModel.Success();
            }

            return ResultViewModel.Error("Não foi possível localizar a manutenção especificada");
        }
    }
}
