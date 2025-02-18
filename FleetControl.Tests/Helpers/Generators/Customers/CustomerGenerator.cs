using Bogus;
using Bogus.Extensions.Brazil;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class CustomerGenerator : FakeDataGeneratorBase<Customer>
    {
        public CustomerGenerator() : base(new Faker<Customer>()
            .CustomInstantiator(f => new Customer(
                f.Company.CompanyName(),
                f.Address.StreetAddress(),
                f.Phone.PhoneNumber("(##) #####-####"),
                f.Company.Cnpj(),
                f.Internet.Email()
            )))
        { }
    }
}
