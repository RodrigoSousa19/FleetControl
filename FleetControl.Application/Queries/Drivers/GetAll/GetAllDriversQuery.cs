using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetAll
{
    public class GetAllDriversQuery : IRequest<ResultViewModel<IList<DriverViewModel>>>
    {
    }
}
