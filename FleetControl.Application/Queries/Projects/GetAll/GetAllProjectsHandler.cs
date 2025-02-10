using FleetControl.Application.Models;
using FleetControl.Application.Models.Projects;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetAll
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<IList<ProjectViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<ProjectViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll(includeNavigation: true);

            var model = projects.Select(ProjectViewModel.FromEntity).ToList();

            return ResultViewModel<IList<ProjectViewModel>>.Success(model);
        }
    }
}
