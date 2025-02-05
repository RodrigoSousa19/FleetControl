using FleetControl.Core.Enums.Reservation;

namespace FleetControl.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public Reservation(int idCustomer, int idDriver, int idVehicle, DateTime startDate, DateTime endDate, string observation)
        {
            IdCustomer = idCustomer;
            IdDriver = idDriver;
            IdVehicle = idVehicle;
            StartDate = startDate;
            EndDate = endDate;
            Observation = observation;

            Status = ReservationStatus.Pending;
            ReservationComments = [];
        }

        public int IdCustomer { get; private set; }
        public Customer Customer { get; set; }
        public int IdDriver { get; private set; }
        public Driver Driver { get; set; }
        public int IdVehicle { get; private set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public string Observation { get; private set; }
        public List<ReservationComment> ReservationComments { get; private set; }

        public void Update(DateTime startDate, DateTime endDate, int idDriver)
        {
            StartDate = startDate;
            EndDate = endDate;
            IdDriver = idDriver;

            UpdatedAt = DateTime.Now;
        }

        public void ConfirmReservation()
        {
            if (Status == ReservationStatus.Pending)
                Status = ReservationStatus.Confirmed;
        }

        public void CancelReservation()
        {
            if (Status != ReservationStatus.Finished)
                Status = ReservationStatus.Canceled;
        }

        public void FinishReservation()
        {
            if (Status == ReservationStatus.Confirmed)
                Status = ReservationStatus.Finished;
        }

        public string GetStatusDescription()
        {
            return Status switch
            {
                ReservationStatus.Pending => "Pendente",
                ReservationStatus.Confirmed => "Confirmada",
                ReservationStatus.Canceled => "Cancelada",
                ReservationStatus.Finished => "Finalizada",
                _ => "Status Desconhecido"
            };
        }
    }
}
