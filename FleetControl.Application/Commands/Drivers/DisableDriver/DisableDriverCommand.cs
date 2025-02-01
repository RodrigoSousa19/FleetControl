using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DisableDriver
{
    public class DisableDriverCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DisableDriverCommand(int id)
        {
            Id = id;
        }
    }
}
