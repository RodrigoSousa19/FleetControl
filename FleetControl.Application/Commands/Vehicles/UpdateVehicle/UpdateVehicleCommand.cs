using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<ResultViewModel>
    {
        public int IdVehicle { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public int MileAge { get; set; }
    }
}
