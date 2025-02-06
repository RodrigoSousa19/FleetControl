using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetVehicleMaintenanceByIdHandler : IRequestHandler<GetVehicleMaintenanceByIdQuery, ResultViewModel<VehicleMaintenanceViewModel>>
    {

        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public GetVehicleMaintenanceByIdHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<VehicleMaintenanceViewModel>> Handle(GetVehicleMaintenanceByIdQuery request, CancellationToken cancellationToken)
        {
            var maintenance = await _repository.GetById(request.Id, includeNavigation: true);

            if (maintenance is null)
                return ResultViewModel<VehicleMaintenanceViewModel>.Error("Não foi possível localizar a manutenção informada.");

            var model = VehicleMaintenanceViewModel.FromEntity(maintenance);

            return ResultViewModel<VehicleMaintenanceViewModel>.Success(model);
        }
    }
}
