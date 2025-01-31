using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Projects.InsertProject
{
    public class InsertProjectCommand : IRequest<ResultViewModel<Project>>
    {
        public string Nome { get; set; }
        public int IdCostCenter { get; set; }
        public int IdCustomer { get; set; }

        public Project ToEntity() => new(Nome, IdCostCenter, IdCustomer);
    }
}
