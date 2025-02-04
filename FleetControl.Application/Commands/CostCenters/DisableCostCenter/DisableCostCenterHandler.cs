using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DisableCostCenter
{
    public class DisableCostCenterHandler : IRequestHandler<DisableCostCenterCommand, ResultViewModel>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public DisableCostCenterHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DisableCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            if (!costCenter.Enabled)
                return ResultViewModel.Error("O centro de custo informado já se está inativo.");

            costCenter.Disable();

            await _repository.Update(costCenter);

            return ResultViewModel.Success();
        }
    }
}
