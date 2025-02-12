using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class DriverProjectsGenerator : FakeDataGeneratorBase<DriverProjects>
    {
        public DriverProjectsGenerator() : base(new Faker<DriverProjects>().
            CustomInstantiator(f => new DriverProjects(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100)
            )))
        {

        }
    }
}
