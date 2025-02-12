using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class VehicleMaintenanceGenerator : FakeDataGeneratorBase<VehicleMaintenance>
    {
        public VehicleMaintenanceGenerator() : base(new Faker<VehicleMaintenance>()
            .CustomInstantiator(f => new VehicleMaintenance(
                f.Random.Int(1, 100),
                f.Lorem.Paragraph(),
                f.Random.Int(1, 100),
                DateTime.Now,
                f.Date.Future(2)
            )))
        { }
    }
}
