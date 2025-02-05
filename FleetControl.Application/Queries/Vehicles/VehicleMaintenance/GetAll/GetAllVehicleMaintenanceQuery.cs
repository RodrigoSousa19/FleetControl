using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetAllVehicleMaintenanceQuery : IRequest<ResultViewModel<IList<VehicleMaintenanceViewModel>>>
    {
    }
}
