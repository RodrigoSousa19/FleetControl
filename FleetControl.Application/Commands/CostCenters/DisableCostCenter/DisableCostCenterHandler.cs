using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DisableCostCenter
{
    public class DisableCostCenterHandler : IRequestHandler<DisableCostCenterCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DisableCostCenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DisableCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            if (!costCenter.Enabled)
                return ResultViewModel.Error("O centro de custo informado já se está inativo.");

            costCenter.Disable();

            await _unitOfWork.CostCenterRepository.Update(costCenter);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
