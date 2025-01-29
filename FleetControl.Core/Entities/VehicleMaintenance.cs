using FleetControl.Core.Enums.Vehicle;

namespace FleetControl.Core.Entities
{
    public class VehicleMaintenance : BaseEntity
    {
        public VehicleMaintenance(int idVehicle, string description, decimal totalCost, DateTime startDate, DateTime endDate)
        {
            IdVehicle = idVehicle;
            Description = description;
            TotalCost = totalCost;
            StartDate = startDate;
            EndDate = endDate;

            Status = MaintenanceStatus.Pending;
        }

        public int IdVehicle { get; private set; }
        public Vehicle Vehicle { get; set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public MaintenanceStatus Status { get; private set; }

        public void Update(string description, decimal totalCost, DateTime startDate, DateTime endDate)
        {
            Description = description;
            TotalCost = totalCost;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void SetInProgress()
        {
            if (Status == MaintenanceStatus.Pending)
                Status = MaintenanceStatus.InProgress;
        }

        public void FinishMaintenance()
        {
            if (Status == MaintenanceStatus.InProgress)
                Status = MaintenanceStatus.Completed;
        }

        public void CancelMaintenance()
        {
            if (Status == MaintenanceStatus.Pending)
                Status = MaintenanceStatus.Canceled;
        }
    }
}
