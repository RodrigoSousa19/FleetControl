using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetAll
{
    public class GetAllVehiclesQuery : IRequest<ResultViewModel<IList<Vehicle>>>
    {
    }
}
