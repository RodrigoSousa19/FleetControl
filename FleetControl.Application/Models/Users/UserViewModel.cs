using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Users
{
    public class UserViewModel
    {
        public UserViewModel(int id, string fullName, string email, bool enabled)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public bool Enabled { get; private set; }

        public static UserViewModel FromEntity(User entity) => new(entity.Id, entity.Name, entity.Email, entity.Enabled);
    }
}
