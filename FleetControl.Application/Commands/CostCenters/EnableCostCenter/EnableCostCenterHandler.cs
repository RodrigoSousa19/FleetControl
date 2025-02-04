using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.EnableCostCenter
{
    public class EnableCostCenterHandler : IRequestHandler<EnableCostCenterCommand, ResultViewModel>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public EnableCostCenterHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(EnableCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            if (costCenter.Enabled)
                return ResultViewModel.Error("O centro de custo informado já se está ativo.");

            costCenter.Enable();

            await _repository.Update(costCenter);

            return ResultViewModel.Success();
        }
    }
}
