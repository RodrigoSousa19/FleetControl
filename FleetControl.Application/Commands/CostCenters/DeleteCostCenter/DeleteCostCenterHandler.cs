using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DeleteCostCenter
{
    public class DeleteCostCenterHandler : IRequestHandler<DeleteCostCenterCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCostCenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            costCenter.SetAsDeleted();

            await _unitOfWork.CostCenterRepository.Update(costCenter);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
