using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleAvailable
{
    public class SetVehicleAvailableCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public SetVehicleAvailableCommand(int id)
        {
            Id = id;
        }
    }
}
