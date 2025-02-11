using Bogus;
using FleetControl.Core.Entities;

namespace FleetControl.Tests.Helpers.Generators
{
    public class ReservationCommentGenerator : FakeDataGeneratorBase<ReservationComment>
    {
        public ReservationCommentGenerator() : base(new Faker<ReservationComment>()
            .CustomInstantiator(f => new ReservationComment(
                f.Lorem.Paragraph(),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100)
            )))
        { }
    }
}
