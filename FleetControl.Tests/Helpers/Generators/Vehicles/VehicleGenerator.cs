using Bogus;
using FleetControl.Core.Entities;
using FleetControl.Tests.FakerExtensions;

namespace FleetControl.Tests.Helpers.Generators
{
    public class VehicleGenerator : FakeDataGeneratorBase<Vehicle>
    {
        public VehicleGenerator() : base(new Faker<Vehicle>()
            .CustomInstantiator(f => new Vehicle(
                f.Vehicle.Manufacturer(),
                f.Vehicle.Model(),
                f.Vehicle.Fuel(),
                f.Vehicle.BrazilLicensePlate(),
                f.Commerce.Color(),
                f.Random.Int(1, 100000))
            ))
        { }
    }
}
