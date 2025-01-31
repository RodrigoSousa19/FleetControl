using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Projects.DisableProject
{
    public class DisableProjectCommandHandler : IRequestHandler<DisableProjectCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Project> _repository;

        public DisableProjectCommandHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DisableProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            if (!project.Enabled)
                return ResultViewModel.Error("O projeto já se encontra inativo.");

            project.Disable();

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
