using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetAll
{
    public class GetAllCostCentersQuery : IRequest<ResultViewModel<IList<CostCenter>>>
    {
    }
}
