using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Projects.UpdateProject
{
    public class UpdateProjectCommand : IRequest<ResultViewModel>
    {
        public int IdProject { get; set; }
        public string Description { get; set; }
        public int IdCostCenter { get; set; }
        public int IdCustomer { get; set; }
    }
}
