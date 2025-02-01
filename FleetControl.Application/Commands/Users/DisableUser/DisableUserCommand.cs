using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Users.DisableUser
{
    public class DisableUserCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DisableUserCommand(int id)
        {
            Id = id;
        }
    }
}
