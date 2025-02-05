using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel<User>>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User ToEntity() => new(Name, Email);
    }
}
