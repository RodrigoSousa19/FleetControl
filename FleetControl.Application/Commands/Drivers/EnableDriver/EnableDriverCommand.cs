using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.EnableDriver
{
    public class EnableDriverCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public EnableDriverCommand(int id)
        {
            Id = id;
        }
    }
}
