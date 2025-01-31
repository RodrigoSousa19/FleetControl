using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.InsertCostCenter
{
    public class InsertCostCenterCommand : IRequest<ResultViewModel<CostCenter>>
    {
        public string Description { get; set; }

        public CostCenter ToEntity() => new(Description);
    }
}
