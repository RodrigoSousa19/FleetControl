using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.EnableVehicle
{
    public class EnableVehicleCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public EnableVehicleCommand(int id)
        {
            Id = id;
        }
    }
}
