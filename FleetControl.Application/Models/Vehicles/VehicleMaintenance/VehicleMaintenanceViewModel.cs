using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Vehicles
{
    public class VehicleMaintenanceViewModel
    {
        public VehicleMaintenanceViewModel(int id, string vehicle, string vehicleLicensePlate, string description, decimal totalCost, DateTime startDate, DateTime endDate, string status)
        {
            Id = id;
            Vehicle = vehicle;
            VehicleLicensePlate = vehicleLicensePlate;
            Description = description;
            TotalCost = totalCost;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

        public int Id { get; private set; }
        public string Vehicle { get; private set; }
        public string VehicleLicensePlate { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Status { get; private set; }

        public VehicleMaintenanceViewModel FromEntity(VehicleMaintenance entity) => new(entity.Id, entity.Vehicle.Model, entity.Vehicle.LicensePlate, entity.Description, entity.TotalCost, entity.StartDate, entity.EndDate, entity.GetStatusDescription());
    }
}
