using Bogus;
using FleetControl.Application.Commands.Reservations.ReservationsComments;

namespace FleetControl.Tests.Helpers.Generators.Reservations.ReservationComment.Commands
{
    public class ReservationCommentCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertReservationCommentCommand> _insertCommandFaker = new Faker<InsertReservationCommentCommand>()
            .RuleFor(c => c.IdReservation, f => f.Random.Int(1, 100))
            .RuleFor(c => c.IdUser, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Content, f => f.Lorem.Paragraph());

        private readonly Faker<DeleteReservationCommentCommand> _deleteCommandFaker = new Faker<DeleteReservationCommentCommand>()
            .CustomInstantiator(f => new DeleteReservationCommentCommand(f.Random.Int(1, 100)));

        public ReservationCommentCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
                {
                    { CommandType.Insert, _insertCommandFaker },
                    { CommandType.Delete, _deleteCommandFaker }
                };
        }
    }
}
