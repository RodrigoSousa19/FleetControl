using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Projects.GetAll
{
    public class GetAllProjects : IRequest<ResultViewModel<IList<Project>>>
    {
    }
}
