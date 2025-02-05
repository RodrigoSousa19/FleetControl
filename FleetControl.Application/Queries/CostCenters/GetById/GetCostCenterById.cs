using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetById
{
    public class GetCostCenterByIdQuery : IRequest<ResultViewModel<CostCenterViewModel>>
    {
        public int Id { get; private set; }

        public GetCostCenterByIdQuery(int id)
        {
            Id = id;
        }
    }
}
