using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.CostCenters.DeleteCostCenter
{
    public class DeleteCostCenterCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteCostCenterCommand(int id)
        {
            Id = id;
        }
    }
}
