using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Users.EnableUser
{
    public class EnableUserCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public EnableUserCommand(int id)
        {
            Id = id;
        }
    }
}
