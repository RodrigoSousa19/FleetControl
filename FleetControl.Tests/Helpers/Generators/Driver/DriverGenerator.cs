using Bogus;
using Bogus.Extensions.Brazil;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using FleetControl.Tests.FakerExtensions;
using FleetControl.Tests.Helpers.Interfaces;

namespace FleetControl.Tests.Helpers.Generators
{
    public class DriverGenerator : IFakeDataGenerator<Driver>
    {
        private readonly Faker<Driver> _cpfFaker;
        private readonly Faker<Driver> _cnhFaker;

        public DriverGenerator()
        {
            _cpfFaker = new Faker<Driver>().CustomInstantiator(f => new Driver(f.Random.Int(1, 100), f.Person.Cpf(), DocumentType.CPF));
            _cnhFaker = new Faker<Driver>().CustomInstantiator(f => new Driver(f.Random.Int(1, 100), f.Person.Cnh(), DocumentType.DriversLicense));
        }

        public Driver GenerateWithCnh() => _cnhFaker.Generate();

        public Driver Generate() => _cpfFaker.Generate();

        public List<Driver> GenerateListWithCnh(int quantity) => _cnhFaker.Generate(quantity);

        public IList<Driver> GenerateList(int quantity) => _cpfFaker.Generate(quantity);
    }
}
