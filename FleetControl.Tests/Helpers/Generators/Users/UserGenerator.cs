using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class UserGenerator : FakeDataGeneratorBase<User>
    {
        public UserGenerator() : base(new Faker<User>()
            .CustomInstantiator(f => new User(
                f.Person.FullName,
                f.Person.Email,
                f.Internet.Password(),
                f.Date.Between(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-18))
            )))
        { }
    }
}
