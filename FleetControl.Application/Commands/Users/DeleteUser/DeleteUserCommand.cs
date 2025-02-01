using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
