using Bogus;
using FleetControl.Tests.Helpers.Interfaces;

namespace FleetControl.Tests.Helpers
{
    public abstract class FakeDataGeneratorBase<T> : IFakeDataGenerator<T> where T : class
    {
        protected readonly Faker<T> _faker;

        protected FakeDataGeneratorBase(Faker<T> faker)
        {
            _faker = faker;
        }

        public T Generate() => _faker.Generate();

        public IList<T> GenerateList(int quantity) => _faker.Generate(quantity);
    }
}
