using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DisableCostCenter
{
    public class DisableCostCenterCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DisableCostCenterCommand(int id)
        {
            Id = id;
        }
    }
}
