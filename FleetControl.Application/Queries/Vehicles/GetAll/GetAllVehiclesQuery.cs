using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetAll
{
    public class GetAllVehiclesQuery : IRequest<ResultViewModel<IList<VehicleViewModel>>>
    {
    }
}
