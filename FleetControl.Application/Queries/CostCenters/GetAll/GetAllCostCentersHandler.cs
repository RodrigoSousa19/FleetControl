using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetAll
{
    public class GetAllCostCentersHandler : IRequestHandler<GetAllCostCentersQuery, ResultViewModel<IList<CostCenterViewModel>>>
    {

        private readonly IGenericRepository<CostCenter> _repository;

        public GetAllCostCentersHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<CostCenterViewModel>>> Handle(GetAllCostCentersQuery request, CancellationToken cancellationToken)
        {
            var costCenters = await _repository.GetAll();

            var model = costCenters.Select(CostCenterViewModel.FromEntity).ToList();

            return ResultViewModel<IList<CostCenterViewModel>>.Success(model);
        }
    }
}
