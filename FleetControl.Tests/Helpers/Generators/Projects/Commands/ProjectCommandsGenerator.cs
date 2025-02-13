using Bogus;
using FleetControl.Application.Commands.Projects.DeleteProject;
using FleetControl.Application.Commands.Projects.DisableProject;
using FleetControl.Application.Commands.Projects.EnableProject;
using FleetControl.Application.Commands.Projects.InsertProject;
using FleetControl.Application.Commands.Projects.UpdateProject;

namespace FleetControl.Tests.Helpers.Generators.Projects.Commands
{
    public class ProjectCommandsGenerator : CommandsGeneratorBase
    {

        private readonly Faker<InsertProjectCommand> _insertCommandFaker = new Faker<InsertProjectCommand>()
            .RuleFor(p => p.IdCustomer, f => f.Random.Int(1, 100))
            .RuleFor(p => p.IdCostCenter, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph());

        private readonly Faker<UpdateProjectCommand> _updateCommandFaker = new Faker<UpdateProjectCommand>()
           .RuleFor(p => p.IdCustomer, f => f.Random.Int(1, 100))
            .RuleFor(p => p.IdCostCenter, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph());

        private readonly Faker<DeleteProjectCommand> _deleteCommandFaker = new Faker<DeleteProjectCommand>().CustomInstantiator(f => new DeleteProjectCommand(f.Random.Int(1, 100)));

        private readonly Faker<EnableProjectCommand> _enableCommandFaker = new Faker<EnableProjectCommand>().CustomInstantiator(f => new EnableProjectCommand(f.Random.Int(1, 100)));

        private readonly Faker<DisableProjectCommand> _disableCommandFaker = new Faker<DisableProjectCommand>().CustomInstantiator(f => new DisableProjectCommand(f.Random.Int(1, 100)));

        public ProjectCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker },
                { CommandType.Update, _updateCommandFaker },
                { CommandType.Delete, _deleteCommandFaker },
                { CommandType.Enable, _enableCommandFaker },
                { CommandType.Disable, _disableCommandFaker }
            };
        }
    }
}
