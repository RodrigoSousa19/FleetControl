using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.UpdateCostCenter
{
    public class UpdateCostCenterHandler : IRequestHandler<UpdateCostCenterCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCostCenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.IdCostCenter);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            new Validator()
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            costCenter.Update(request.Description);

            await _unitOfWork.CostCenterRepository.Update(costCenter);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
