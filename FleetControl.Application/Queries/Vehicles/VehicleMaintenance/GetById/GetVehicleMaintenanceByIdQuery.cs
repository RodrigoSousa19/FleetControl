using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetVehicleMaintenanceByIdQuery : IRequest<ResultViewModel<VehicleMaintenanceViewModel>>
    {
        public int Id { get; private set; }

        public GetVehicleMaintenanceByIdQuery(int id)
        {
            Id = id;
        }
    }
}
