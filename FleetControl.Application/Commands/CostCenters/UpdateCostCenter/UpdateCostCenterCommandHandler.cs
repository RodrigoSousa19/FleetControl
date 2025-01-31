using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.UpdateCostCenter
{
    public class UpdateCostCenterCommandHandler : IRequestHandler<UpdateCostCenterCommand, ResultViewModel>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public UpdateCostCenterCommandHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.GetById(request.IdCostCenter);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            costCenter.Update(request.Description);

            await _repository.Update(costCenter);

            return ResultViewModel.Success();
        }
    }
}
