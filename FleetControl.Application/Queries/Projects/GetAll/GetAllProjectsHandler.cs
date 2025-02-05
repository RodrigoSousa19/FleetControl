using FleetControl.Application.Models;
using FleetControl.Application.Models.Projects;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetAll
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<IList<ProjectViewModel>>>
    {

        private readonly IGenericRepository<Project> _repository;

        public GetAllProjectsHandler(IGenericRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<ProjectViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll(includeNavigation: true);

            var model = projects.Select(ProjectViewModel.FromEntity).ToList();

            return ResultViewModel<IList<ProjectViewModel>>.Success(model);
        }
    }
}
