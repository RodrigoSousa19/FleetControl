using FleetControl.Application.Models;
using FleetControl.Application.Models.Projects;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetById
{
    public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
    {
        public int Id { get; private set; }

        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
