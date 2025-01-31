using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DeleteCostCenter
{
    public class DeleteCostCenterCommandHandler : IRequestHandler<DeleteCostCenterCommand, ResultViewModel>
    {
        private readonly IGenericRepository<CostCenter> _repository;
        public DeleteCostCenterCommandHandler(IGenericRepository<CostCenter> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = await _repository.GetById(request.Id);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo informado.");

            costCenter.SetAsDeleted();

            await _repository.Update(costCenter);

            return ResultViewModel.Success();
        }
    }
}
