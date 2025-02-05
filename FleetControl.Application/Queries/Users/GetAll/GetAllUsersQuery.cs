using FleetControl.Application.Models;
using FleetControl.Application.Models.Users;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<ResultViewModel<IList<UserViewModel>>>
    {
    }
}
