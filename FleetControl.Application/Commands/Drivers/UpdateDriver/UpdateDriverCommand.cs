using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.UpdateDriver
{
    public class UpdateDriverCommand : IRequest<ResultViewModel>
    {
        public int IdDriver { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
    }
}
