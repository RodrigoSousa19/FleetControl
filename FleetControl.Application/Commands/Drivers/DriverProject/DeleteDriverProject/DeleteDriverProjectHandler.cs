using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class DeleteDriverProjectHandler : IRequestHandler<DeleteDriverProjectCommand, ResultViewModel>
    {
        private readonly IGenericRepository<DriverProjects> _repository;

        public DeleteDriverProjectHandler(IGenericRepository<DriverProjects> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteDriverProjectCommand request, CancellationToken cancellationToken)
        {
            var driverProject = await _repository.GetById(request.Id);

            if (driverProject is null)
                return ResultViewModel.Error("Não foi possível encontrar a associação de motorista e projeto informada.");

            driverProject.SetAsDeleted();

            await _repository.Update(driverProject);

            return ResultViewModel.Success();
        }
    }
}
