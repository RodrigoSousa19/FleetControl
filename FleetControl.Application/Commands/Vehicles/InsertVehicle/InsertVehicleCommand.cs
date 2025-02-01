using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.InsertVehicle
{
    public class InsertVehicleCommand : IRequest<ResultViewModel<Vehicle>>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public int MileAge { get; set; }

        public Vehicle ToEntity() => new(Brand, Model, FuelType, LicensePlate, Color, MileAge);
    }
}
