using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class UpdateDriverProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public int IdDriver { get; set; }
        public int IdProject { get; set; }
    }
}
