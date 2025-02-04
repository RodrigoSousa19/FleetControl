using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class UpdateMaintenanceCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
