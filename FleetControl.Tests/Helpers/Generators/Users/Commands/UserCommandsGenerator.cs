using Bogus;
using FleetControl.Application.Commands.Users.DeleteUser;
using FleetControl.Application.Commands.Users.DisableUser;
using FleetControl.Application.Commands.Users.EnableUser;
using FleetControl.Application.Commands.Users.InsertUser;
using FleetControl.Application.Commands.Users.UpdateUser;

namespace FleetControl.Tests.Helpers.Generators.Users.Commands
{
    public class UserCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertUserCommand> _insertCommandFaker = new Faker<InsertUserCommand>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.Lorem.Word())
            .RuleFor(u => u.BirthDate, f => f.Date.Between(DateTime.Now.Date.AddYears(-50), DateTime.Now.Date.AddYears(-18)));

        private readonly Faker<UpdateUserCommand> _updateCommandFaker = new Faker<UpdateUserCommand>()
            .RuleFor(u => u.IdUser, f => f.Random.Int(1, 100))
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Role, f => f.Lorem.Word())
            .RuleFor(u => u.BirthDate, f => f.Date.Between(DateTime.Now.Date.AddYears(-50), DateTime.Now.Date.AddYears(-18)));

        private readonly Faker<DeleteUserCommand> _deleteCommandFaker = new Faker<DeleteUserCommand>().CustomInstantiator(f => new DeleteUserCommand(f.Random.Int(1, 100)));
        private readonly Faker<EnableUserCommand> _enableCommandFaker = new Faker<EnableUserCommand>().CustomInstantiator(f => new EnableUserCommand(f.Random.Int(1, 100)));
        private readonly Faker<DisableUserCommand> _disableCommandFaker = new Faker<DisableUserCommand>().CustomInstantiator(f => new DisableUserCommand(f.Random.Int(1, 100)));

        public UserCommandsGenerator()
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
