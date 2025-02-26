using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Users.Login
{
    public class LoginCommand : IRequest<ResultViewModel<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
