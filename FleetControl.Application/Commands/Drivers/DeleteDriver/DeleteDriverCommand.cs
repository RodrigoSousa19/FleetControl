using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DeleteDriver
{
    public class DeleteDriverCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteDriverCommand(int id)
        {
            Id = id;
        }
    }
}
