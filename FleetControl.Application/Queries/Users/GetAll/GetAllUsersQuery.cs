using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<ResultViewModel<IList<User>>>
    {
    }
}
