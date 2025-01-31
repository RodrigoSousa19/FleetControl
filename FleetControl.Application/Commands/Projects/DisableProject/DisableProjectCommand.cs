using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Projects.DisableProject
{
    public class DisableProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DisableProjectCommand(int id)
        {
            Id = id;
        }
    }
}
