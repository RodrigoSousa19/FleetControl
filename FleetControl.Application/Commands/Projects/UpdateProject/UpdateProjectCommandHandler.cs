using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Projects.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Project> _repository;

        public UpdateProjectCommandHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            project.Update(request.Nome, request.IdCostCenter, request.IdCustomer);

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
