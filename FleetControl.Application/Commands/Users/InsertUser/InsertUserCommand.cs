using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }


        public User ToEntity() => new(Name, Email,Password,Role,BirthDate);
    }
}
