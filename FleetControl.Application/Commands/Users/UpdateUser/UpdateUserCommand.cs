using FleetControl.Application.Models;
using FleetControl.Core.Enums.User;
using MediatR;

namespace FleetControl.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
