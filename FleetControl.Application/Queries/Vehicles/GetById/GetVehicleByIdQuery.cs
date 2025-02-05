using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetById
{

    public class GetVehicleByIdQuery : IRequest<ResultViewModel<VehicleViewModel>>
    {
        public int Id { get; private set; }

        public GetVehicleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
