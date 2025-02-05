using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetById
{
    public class GetDriverByIdQuery : IRequest<ResultViewModel<DriverViewModel>>
    {
        public int Id { get; private set; }

        public GetDriverByIdQuery(int id)
        {
            Id = id;
        }
    }
}
