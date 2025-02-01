using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class InsertDriverProjectCommand : IRequest<ResultViewModel<DriverProjects>>
    {
        public int IdProject { get; set; }
        public int IdDriver { get; set; }

        public DriverProjects ToEntity() => new(IdProject, IdDriver);
    }
}
