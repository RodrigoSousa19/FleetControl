using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetById
{
    public class GetCostCenterByIdHandler : IRequestHandler<GetCostCenterByIdQuery, ResultViewModel<CostCenterViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetCostCenterByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<CostCenterViewModel>> Handle(GetCostCenterByIdQuery request, CancellationToken cancellationToken)
        {
            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel<CostCenterViewModel>.Error("Não foi possível localizar o centro de custo informado.");

            var model = CostCenterViewModel.FromEntity(costCenter);

            return ResultViewModel<CostCenterViewModel>.Success(model);
        }
    }
}
