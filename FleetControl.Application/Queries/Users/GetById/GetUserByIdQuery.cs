using FleetControl.Application.Models;
using FleetControl.Application.Models.Users;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
    {
        public int Id { get; private set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
