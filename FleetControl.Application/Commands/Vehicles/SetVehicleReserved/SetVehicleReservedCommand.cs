using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleReserved
{
    public class SetVehicleReservedCommand : IRequest<ResultViewModel>
    {
        public int IdVehicle { get; set; }
        public int IdProject { get; set; }
    }
}
