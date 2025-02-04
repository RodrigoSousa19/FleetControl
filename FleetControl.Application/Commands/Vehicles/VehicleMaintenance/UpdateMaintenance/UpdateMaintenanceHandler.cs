using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class UpdateMaintenanceHandler : IRequestHandler<UpdateMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public UpdateMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _repository.GetById(request.Id);

            if (maintenance is not null)
            {
                maintenance.Update(request.Description, request.TotalCost, request.StartDate, request.EndDate);

                await _repository.Update(maintenance);

                return ResultViewModel.Success();
            }

            return ResultViewModel.Error("Não foi possível localizar a manutenção especificada");
        }
    }
}
