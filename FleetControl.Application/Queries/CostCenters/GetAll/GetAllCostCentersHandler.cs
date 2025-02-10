using FleetControl.Application.Models;
using FleetControl.Application.Models.CostCenters;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.CostCenters.GetAll
{
    public class GetAllCostCentersHandler : IRequestHandler<GetAllCostCentersQuery, ResultViewModel<IList<CostCenterViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllCostCentersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<CostCenterViewModel>>> Handle(GetAllCostCentersQuery request, CancellationToken cancellationToken)
        {
            var costCenters = await _unitOfWork.CostCenterRepository.GetAll();

            var model = costCenters.Select(CostCenterViewModel.FromEntity).ToList();

            return ResultViewModel<IList<CostCenterViewModel>>.Success(model);
        }
    }
}
