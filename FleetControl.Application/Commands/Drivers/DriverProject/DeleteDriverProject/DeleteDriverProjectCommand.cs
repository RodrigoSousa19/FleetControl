using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class DeleteDriverProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteDriverProjectCommand(int id)
        {
            Id = id;
        }
    }
}
