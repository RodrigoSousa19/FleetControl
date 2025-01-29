namespace FleetControl.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;

            Comments = [];
        }

        public string Name { get; private set; }
        public string Email { get; private set; }

        public List<ReservationComment> Comments { get; private set; }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
