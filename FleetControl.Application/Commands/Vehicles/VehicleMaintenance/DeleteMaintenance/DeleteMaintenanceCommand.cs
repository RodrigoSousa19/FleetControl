using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class DeleteMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteMaintenanceCommand(int id)
        {
            Id = id;
        }
    }
}
