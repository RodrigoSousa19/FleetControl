using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetAll
{
    public class GetAllDriversQuery : IRequest<ResultViewModel<IList<Driver>>>
    {
    }
}
