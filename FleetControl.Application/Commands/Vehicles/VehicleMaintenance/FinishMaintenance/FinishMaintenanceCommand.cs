using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class FinishMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public FinishMaintenanceCommand(int id)
        {
            Id = id;
        }
    }
}
