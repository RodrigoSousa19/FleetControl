using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Projects.EnableProject
{
    public class EnableProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public EnableProjectCommand(int id)
        {
            Id = id;
        }
    }
}
