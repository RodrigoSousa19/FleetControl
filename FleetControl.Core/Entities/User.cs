namespace FleetControl.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email, string password, string role, DateTime birthDate)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            BirthDate = birthDate;

            Comments = [];
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }

        public List<ReservationComment> Comments { get; private set; }

        public void Update(string name, string email, string role, DateTime birthDate)
        {
            Name = name;
            Email = email;
            Role = role;
            BirthDate = birthDate;

            UpdatedAt = DateTime.Now;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}
