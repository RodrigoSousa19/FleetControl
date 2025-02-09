using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.InsertCostCenter
{
    public class InsertCostCenterHandler : IRequestHandler<InsertCostCenterCommand, ResultViewModel<CostCenter>>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public InsertCostCenterHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<CostCenter>> Handle(InsertCostCenterCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Description, ErrorsList.InvalidDescription)
                .Validate();

            var costCenter = await _repository.Create(request.ToEntity());

            return ResultViewModel<CostCenter>.Success(costCenter);
        }
    }
}
