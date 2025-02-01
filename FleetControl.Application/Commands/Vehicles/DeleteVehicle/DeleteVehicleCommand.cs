using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteVehicleCommand(int id)
        {
            Id = id;
        }
    }
}
