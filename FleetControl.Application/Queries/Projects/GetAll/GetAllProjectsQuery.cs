using FleetControl.Application.Models;
using FleetControl.Application.Models.Projects;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetAll
{
    public class GetAllProjects : IRequest<ResultViewModel<IList<ProjectViewModel>>>
    {
    }
}
