using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetAllVehicleMaintenanceHandler : IRequestHandler<GetAllVehicleMaintenanceQuery, ResultViewModel<IList<VehicleMaintenanceViewModel>>>
    {

        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public GetAllVehicleMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<VehicleMaintenanceViewModel>>> Handle(GetAllVehicleMaintenanceQuery request, CancellationToken cancellationToken)
        {
            var maintenances = await _repository.GetAll(includeNavigation: true);

            var model = maintenances.Select(VehicleMaintenanceViewModel.FromEntity).ToList();

            return ResultViewModel<IList<VehicleMaintenanceViewModel>>.Success(model);
        }
    }
}
