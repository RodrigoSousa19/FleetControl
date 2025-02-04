using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class StartMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public StartMaintenanceCommand(int id)
        {
            Id = id;
        }
    }
}
