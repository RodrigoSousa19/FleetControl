using FleetControl.Application.Models;
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
            var maintenance = await _repository.Create(request.ToEntity());

            return ResultViewModel<VehicleMaintenance>.Success(maintenance);
        }
    }
}
