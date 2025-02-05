using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetById
{
    public class GetCostCenterByIdHandler : IRequestHandler<GetCostCenterByIdQuery, ResultViewModel<CostCenterViewModel>>
    {

        private readonly IGenericRepository<CostCenter> _repository;

        public GetCostCenterByIdHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<CostCenterViewModel>> Handle(GetCostCenterByIdQuery request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel<CostCenterViewModel>.Error("Não foi possível localizar o centro de custo informado.");

            var model = CostCenterViewModel.FromEntity(costCenter);

            return ResultViewModel<CostCenterViewModel>.Success(model);
        }
    }
}
