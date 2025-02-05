using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Vehicles
{
    public class VehicleViewModel
    {
        public VehicleViewModel(int id, string brand, string model, string fuelType, string licensePlate, string color, int mileAge, string status, DateTime? lastMaintenance, string currentProject)
        {
            Id = Id;
            Brand = brand;
            Model = model;
            FuelType = fuelType;
            LicensePlate = licensePlate;
            Color = color;
            MileAge = mileAge;
            Status = status;
            LastMaintenance = lastMaintenance;
            CurrentProject = currentProject;
        }
        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string FuelType { get; private set; }
        public string LicensePlate { get; private set; }
        public string Color { get; private set; }
        public int MileAge { get; private set; }
        public string Status { get; private set; }
        public DateTime? LastMaintenance { get; private set; }
        public string CurrentProject { get; private set; }

        public static VehicleViewModel FromEntity(Vehicle entity) => new(entity.Id,
                                                                         entity.Brand,
                                                                         entity.Model,
                                                                         entity.FuelType,
                                                                         entity.LicensePlate,
                                                                         entity.Color,
                                                                         entity.MileAge,
                                                                         entity.GetStatusDescription(),
                                                                         entity.LastMaintenance,
                                                                         entity.Project?.Name);
    }
}
