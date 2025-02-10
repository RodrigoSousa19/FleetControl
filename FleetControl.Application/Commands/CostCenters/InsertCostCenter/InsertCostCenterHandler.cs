using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.InsertCostCenter
{
    public class InsertCostCenterHandler : IRequestHandler<InsertCostCenterCommand, ResultViewModel<CostCenter>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public InsertCostCenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel<CostCenter>> Handle(InsertCostCenterCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            var costCenter = await _unitOfWork.CostCenterRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<CostCenter>.Success(costCenter);
        }
    }
}
