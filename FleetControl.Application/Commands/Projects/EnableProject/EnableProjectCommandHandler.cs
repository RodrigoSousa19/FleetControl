using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Projects.EnableProject
{
    public class EnableProjectCommandHandler : IRequestHandler<EnableProjectCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Project> _repository;

        public EnableProjectCommandHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(EnableProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            if (project.Enabled)
                return ResultViewModel.Error("O projeto se encontra ativo.");

            project.Enable();

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
