using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class CancelMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public CancelMaintenanceCommand(int id)
        {
            Id = id;
        }
    }
}
