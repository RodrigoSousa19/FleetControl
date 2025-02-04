using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class InsertMaintenanceCommand : IRequest<ResultViewModel<VehicleMaintenance>>
    {
        public int IdVehicle { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public VehicleMaintenance ToEntity() => new(IdVehicle, Description, TotalCost, StartDate, EndDate);
    }
}
