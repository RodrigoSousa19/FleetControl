using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Projects.InsertProject
{
    public class InsertProjectCommandHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<Project>>
    {
        private readonly IGenericRepository<Project> _repository;

        public InsertProjectCommandHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Project>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.Create(request.ToEntity());

            return ResultViewModel<Project>.Success(project);
        }
    }
}
