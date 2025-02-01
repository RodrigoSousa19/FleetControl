using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel<User>>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User ToEntity() => new(Name, Email);
    }
}
