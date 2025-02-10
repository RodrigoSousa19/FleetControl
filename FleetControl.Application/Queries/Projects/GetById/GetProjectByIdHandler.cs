using FleetControl.Application.Models;
using FleetControl.Application.Models.Projects;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id, includeNavigation: true);

            if (project is null)
                return ResultViewModel<ProjectViewModel>.Error("Não foi possível localizar o projeto informado.");

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
