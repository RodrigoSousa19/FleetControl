using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.UpdateCostCenter
{
    public class UpdateCostCenterCommand : IRequest<ResultViewModel>
    {
        public int IdCostCenter { get; set; }
        public string Description { get; set; }
    }
}
