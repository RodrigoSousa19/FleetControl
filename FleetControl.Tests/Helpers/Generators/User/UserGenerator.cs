using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class UserGenerator : FakeDataGeneratorBase<User>
    {
        public UserGenerator() : base(new Faker<User>()
            .CustomInstantiator(f => new User(
                f.Person.FullName,
                f.Person.Email
            )))
        { }
    }
}
