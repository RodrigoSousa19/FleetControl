using Bogus;
using FleetControl.Application.Commands.Vehicles;

namespace FleetControl.Tests.Helpers.Generators.Vehicles.VehicleMaintenance.Commands
{
    public class VehicleMaintenanceCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertMaintenanceCommand> _insertCommandFaker = new Faker<InsertMaintenanceCommand>()
            .RuleFor(m => m.IdVehicle, f => f.Random.Int(1, 100))
            .RuleFor(m => m.TotalCost, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.StartDate, f => f.Date.Soon(1))
            .RuleFor(m => m.EndDate, f => f.Date.Soon(20));


        private readonly Faker<UpdateMaintenanceCommand> _updateCommandFaker = new Faker<UpdateMaintenanceCommand>()
            .RuleFor(m => m.Id, f => f.Random.Int(1, 100))
            .RuleFor(m => m.TotalCost, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.StartDate, f => f.Date.Soon(1))
            .RuleFor(m => m.EndDate, f => f.Date.Soon(20));


        private readonly Faker<DeleteMaintenanceCommand> _deleteCommandFaker = new Faker<DeleteMaintenanceCommand>()
            .CustomInstantiator(f => new DeleteMaintenanceCommand(f.Random.Int(1, 100)));
        private readonly Faker<CancelMaintenanceCommand> _cancelCommandFaker = new Faker<CancelMaintenanceCommand>()
            .CustomInstantiator(f => new CancelMaintenanceCommand(f.Random.Int(1, 100)));
        private readonly Faker<FinishMaintenanceCommand> _finishCommandFaker = new Faker<FinishMaintenanceCommand>()
            .CustomInstantiator(f => new FinishMaintenanceCommand(f.Random.Int(1, 100)));
        private readonly Faker<StartMaintenanceCommand> _startCommandFaker = new Faker<StartMaintenanceCommand>()
            .CustomInstantiator(f => new StartMaintenanceCommand(f.Random.Int(1, 100)));

        public VehicleMaintenanceCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker.Generate() },
                { CommandType.Update, _updateCommandFaker.Generate() },
                { CommandType.Delete, _deleteCommandFaker.Generate() },
                { CommandType.Cancel, _cancelCommandFaker.Generate() },
                { CommandType.Finish, _finishCommandFaker.Generate() },
                { CommandType.Start, _startCommandFaker.Generate() }
            };
        }
    }
}
