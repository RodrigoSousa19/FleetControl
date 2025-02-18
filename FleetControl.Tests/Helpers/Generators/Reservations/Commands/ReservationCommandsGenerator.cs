using Bogus;
using FleetControl.Application.Commands.Reservations.CancelReservation;
using FleetControl.Application.Commands.Reservations.ConfirmReservation;
using FleetControl.Application.Commands.Reservations.DeleteReservation;
using FleetControl.Application.Commands.Reservations.FinishReservation;
using FleetControl.Application.Commands.Reservations.InsertReservation;
using FleetControl.Application.Commands.Reservations.UpdateReservation;

namespace FleetControl.Tests.Helpers.Generators.Reservations.Commands
{
    public class ReservationCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertReservationCommand> _insertCommandFaker = new Faker<InsertReservationCommand>()
            .RuleFor(r => r.IdProject, f => f.Random.Int(1, 100))
            .RuleFor(r => r.IdVehicle, f => f.Random.Int(1, 100))
            .RuleFor(r => r.IdDriver, f => f.Random.Int(1, 100))
            .RuleFor(r => r.StartDate, f => f.Date.Recent(1))
            .RuleFor(r => r.EndDate, f => f.Date.Soon(1))
            .RuleFor(r => r.Observation, f => f.Lorem.Paragraph());

        private readonly Faker<UpdateReservationCommand> _updateCommandFaker = new Faker<UpdateReservationCommand>()
            .RuleFor(r => r.IdDriver, f => f.Random.Int(1, 100))
            .RuleFor(r => r.StartDate, f => f.Date.Soon(1))
            .RuleFor(r => r.EndDate, f => f.Date.Soon(3));

        private readonly Faker<DeleteReservationCommand> _deleteCommandFaker = new Faker<DeleteReservationCommand>().CustomInstantiator(f => new DeleteReservationCommand(f.Random.Int(1, 100)));
        private readonly Faker<CancelReservationCommand> _cancelCommandFaker = new Faker<CancelReservationCommand>().CustomInstantiator(f => new CancelReservationCommand(f.Random.Int(1, 100)));
        private readonly Faker<ConfirmReservationCommand> _confirmCommandFaker = new Faker<ConfirmReservationCommand>().CustomInstantiator(f => new ConfirmReservationCommand(f.Random.Int(1, 100)));
        private readonly Faker<FinishReservationCommand> _finishCommandFaker = new Faker<FinishReservationCommand>().CustomInstantiator(f => new FinishReservationCommand(f.Random.Int(1, 100)));


        public ReservationCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker.Generate() },
                { CommandType.Update, _updateCommandFaker.Generate() },
                { CommandType.Delete, _deleteCommandFaker.Generate() },
                { CommandType.Cancel, _cancelCommandFaker.Generate() },
                { CommandType.Confirm, _confirmCommandFaker.Generate() },
                { CommandType.Finish, _finishCommandFaker.Generate() }
            };
        }
    }
}
