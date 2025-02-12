using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class ProjectGenerator : FakeDataGeneratorBase<Project>
    {
        public ProjectGenerator() : base(new Faker<Project>()
            .CustomInstantiator(f => new Project(
                f.Commerce.ProductName(),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100)
            )))
        { }
    }
}
