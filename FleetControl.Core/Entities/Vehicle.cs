using FleetControl.Core.Enums.Vehicle;

namespace FleetControl.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public Vehicle(string brand, string model, string fuelType, string licensePlate, string color, int mileAge)
        {
            Brand = brand;
            Model = model;
            FuelType = fuelType;
            LicensePlate = licensePlate;
            Color = color;
            MileAge = mileAge;

            Status = VehicleStatus.Available;

            Enabled = true;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string FuelType { get; private set; }
        public string LicensePlate { get; private set; }
        public string Color { get; private set; }
        public int MileAge { get; private set; }
        public VehicleStatus Status { get; private set; }
        public DateTime? LastMaintenance { get; private set; }
        public int? IdProject { get; private set; }
        public Project Project { get; set; }

        public void Update(string brand, string model, string fuelType, string licensePlate, string color, int mileAge)
        {
            Brand = brand;
            Model = model;
            FuelType = fuelType;
            LicensePlate = licensePlate;
            Color = color;
            MileAge = mileAge;

            UpdatedAt = DateTime.Now;
        }

        public void SendToMaintenance()
        {
            if (Status != VehicleStatus.Reserved)
            {
                LastMaintenance = DateTime.Now;
                Status = VehicleStatus.InMaintenance;
            }
        }

        public void SetReserved(int idProject)
        {
            if (Status == VehicleStatus.Available)
            {
                IdProject = idProject;
                Status = VehicleStatus.Reserved;
            }
        }

        public void SetAvailable()
        {
            Status = VehicleStatus.Available;

            if (IdProject is not null)
                IdProject = null;
        }

        public string GetStatusDescription()
        {
            return Status switch
            {
                VehicleStatus.Available => "Disponível",
                VehicleStatus.Reserved => "Reservado",
                VehicleStatus.InMaintenance => "Em manutenção",
                _ => "Status Desconhecido"
            };
        }
    }
}
