using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SendVehicleToMaintenance
{
    public class SendVehicleToMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public SendVehicleToMaintenanceCommand(int id)
        {
            Id = id;
        }
    }
}
