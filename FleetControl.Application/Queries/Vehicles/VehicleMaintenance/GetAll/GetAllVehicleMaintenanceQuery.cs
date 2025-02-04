using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetAllVehicleMaintenanceQuery : IRequest<ResultViewModel<IList<VehicleMaintenance>>>
    {
    }
}
