using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.InsertCostCenter
{
    public class InsertCostCenterCommandHandler : IRequestHandler<InsertCostCenterCommand, ResultViewModel<CostCenter>>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public InsertCostCenterCommandHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<CostCenter>> Handle(InsertCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.Create(request.ToEntity());

            return ResultViewModel<CostCenter>.Success(costCenter);
        }
    }
}
