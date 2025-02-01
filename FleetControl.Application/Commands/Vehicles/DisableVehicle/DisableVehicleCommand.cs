using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.DisableVehicle
{
    public class DisableVehicleCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DisableVehicleCommand(int id)
        {
            Id = id;
        }
    }
}
