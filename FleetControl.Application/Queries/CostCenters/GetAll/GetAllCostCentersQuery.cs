using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetAll
{
    public class GetAllCostCentersQuery : IRequest<ResultViewModel<IList<CostCenterViewModel>>>
    {
    }
}
