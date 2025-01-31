using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Projects.DeleteProject
{
    public class DeleteProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
    }
}
