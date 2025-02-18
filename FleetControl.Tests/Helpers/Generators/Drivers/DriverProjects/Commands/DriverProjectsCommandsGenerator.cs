using Bogus;
using FleetControl.Application.Commands.Drivers.DriverProject;

namespace FleetControl.Tests.Helpers.Generators.Drivers.DriverProjects.Commands
{
    public class DriverProjectsCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertDriverProjectCommand> _insertCommandFaker = new Faker<InsertDriverProjectCommand>()
            .RuleFor(d => d.IdProject, f => f.Random.Int(1, 100))
            .RuleFor(d => d.IdDriver, f => f.Random.Int());

        private readonly Faker<UpdateDriverProjectCommand> _updateCommandFaker = new Faker<UpdateDriverProjectCommand>()
            .RuleFor(d => d.Id, f => f.Random.Int(1, 100))
            .RuleFor(d => d.IdProject, f => f.Random.Int(1, 100))
            .RuleFor(d => d.IdDriver, f => f.Random.Int());

        private readonly Faker<DeleteDriverProjectCommand> _deleteCommandFaker = new Faker<DeleteDriverProjectCommand>().CustomInstantiator(f => new DeleteDriverProjectCommand(f.Random.Int(1, 100)));

        public DriverProjectsCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker.Generate() },
                { CommandType.Update, _updateCommandFaker.Generate() },
                { CommandType.Delete, _deleteCommandFaker.Generate() }
            };
        }
    }
}
