using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
