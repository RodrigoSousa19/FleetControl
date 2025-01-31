using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Projects.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Project> _repository;

        public DeleteProjectCommandHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            project.SetAsDeleted();

            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
