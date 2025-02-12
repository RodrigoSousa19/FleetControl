using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class ReservationGenerator : FakeDataGeneratorBase<Reservation>
    {
        public ReservationGenerator() : base(new Faker<Reservation>()
            .CustomInstantiator(f => new Reservation(
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                DateTime.Now,
                f.Date.Future(2),
                f.Lorem.Paragraph()
            )))
        { }
    }
}
