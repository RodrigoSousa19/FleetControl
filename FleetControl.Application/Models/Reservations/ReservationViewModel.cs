using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Reservations
{
    public class ReservationViewModel
    {
        public ReservationViewModel(int id, string customerName, string costCenter, string driverName, string vehicle, string vehiclePlate, DateTime startDate, DateTime endDate, string status, string observation, IReadOnlyList<string> comments)
        {
            Id = id;
            CustomerName = customerName;
            CostCenter = costCenter;
            DriverName = driverName;
            Vehicle = vehicle;
            VehiclePlate = vehiclePlate;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Observation = observation;
            Comments = comments;
        }

        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public string CostCenter { get; private set; }
        public string DriverName { get; private set; }
        public string Vehicle { get; private set; }
        public string VehiclePlate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Status { get; private set; }
        public string Observation { get; private set; }
        public IReadOnlyList<string> Comments { get; private set; }

        public static ReservationViewModel FromEntity(Reservation entity)
        {
            var comments = entity.ReservationComments.Select(x => x.Content).ToList();

            return new ReservationViewModel(entity.Id,
                                            entity.Project.Customer?.Name,
                                            entity.Project.CostCenter?.Description,
                                            entity.Driver.User.Name,
                                            entity.Vehicle.Model,
                                            entity.Vehicle.LicensePlate,
                                            entity.StartDate,
                                            entity.EndDate,
                                            entity.GetStatusDescription(),
                                            entity.Observation,
                                            comments);
        }
    }
}
