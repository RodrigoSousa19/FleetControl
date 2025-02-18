using Bogus;
using FleetControl.Application.Commands.Drivers.DeleteDriver;
using FleetControl.Application.Commands.Drivers.DisableDriver;
using FleetControl.Application.Commands.Drivers.EnableDriver;
using FleetControl.Application.Commands.Drivers.InsertDriver;
using FleetControl.Application.Commands.Drivers.UpdateDriver;
using FleetControl.Core.Enums.User;
using FleetControl.Tests.FakerExtensions;

namespace FleetControl.Tests.Helpers.Generators.Drivers.Commands
{
    public class DriverCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertDriverCommand> _insertCommandFaker = new Faker<InsertDriverCommand>()
            .RuleFor(d => d.IdUser, f => f.Random.Int(1, 100))
            .RuleFor(d => d.DocumentNumber, f => f.Person.Cnh())
            .RuleFor(d => d.DocumentType, f => f.PickRandomWithout<DocumentType>(DocumentType.CPF));

        private readonly Faker<UpdateDriverCommand> _updateCommandFaker = new Faker<UpdateDriverCommand>()
            .RuleFor(d => d.IdDriver, f => f.Random.Int(1, 100))
            .RuleFor(d => d.DocumentNumber, f => f.Person.Cnh())
            .RuleFor(d => d.DocumentType, f => f.PickRandomWithout<DocumentType>(DocumentType.CPF));

        private readonly Faker<DeleteDriverCommand> _deleteCommandFaker = new Faker<DeleteDriverCommand>().CustomInstantiator(f => new DeleteDriverCommand(f.Random.Int(1, 100)));
        private readonly Faker<EnableDriverCommand> _enableCommandFaker = new Faker<EnableDriverCommand>().CustomInstantiator(f => new EnableDriverCommand(f.Random.Int(1, 100)));
        private readonly Faker<DisableDriverCommand> _disableCommandFaker = new Faker<DisableDriverCommand>().CustomInstantiator(f => new DisableDriverCommand(f.Random.Int(1, 100)));

        public DriverCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker.Generate() },
                { CommandType.Update, _updateCommandFaker.Generate() },
                { CommandType.Delete, _deleteCommandFaker.Generate() },
                { CommandType.Enable, _enableCommandFaker.Generate() },
                { CommandType.Disable, _disableCommandFaker.Generate() }
            };
        }
    }
}
