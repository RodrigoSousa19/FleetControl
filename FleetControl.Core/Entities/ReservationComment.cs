namespace FleetControl.Core.Entities
{
    public class ReservationComment : BaseEntity
    {
        public ReservationComment(string content, int idReservation, int idUser)
        {
            Content = content;
            IdReservation = idReservation;
            IdUser = idUser;
        }

        public string Content { get; private set; }
        public int IdReservation { get; private set; }
        public Reservation Reservation { get; set; }
        public int IdUser { get; private set; }
        public User User { get; set; }
    }
}
