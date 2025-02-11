using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class CostCenterGenerator : FakeDataGeneratorBase<CostCenter>
    {
        public CostCenterGenerator() : base(new Faker<CostCenter>()
            .CustomInstantiator(f => new CostCenter(f.Company.Random.AlphaNumeric(10))))
        { }
    }
}
