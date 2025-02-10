using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.EnableCostCenter
{
    public class EnableCostCenterHandler : IRequestHandler<EnableCostCenterCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnableCostCenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(EnableCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            if (costCenter.Enabled)
                return ResultViewModel.Error("O centro de custo informado já se está ativo.");

            costCenter.Enable();

            await _unitOfWork.CostCenterRepository.Update(costCenter);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
