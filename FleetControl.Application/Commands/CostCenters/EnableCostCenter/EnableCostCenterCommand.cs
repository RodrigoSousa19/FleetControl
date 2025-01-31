using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.EnableCostCenter
{
    public class EnableCostCenterCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public EnableCostCenterCommand(int id)
        {
            Id = id;
        }
    }
}
