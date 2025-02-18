using Bogus;
using Bogus.Extensions.Brazil;
using FleetControl.Application.Commands.Customers.DeleteCustomer;
using FleetControl.Application.Commands.Customers.DisableCustomer;
using FleetControl.Application.Commands.Customers.EnableCustomer;
using FleetControl.Application.Commands.Customers.InsertCustomer;
using FleetControl.Application.Commands.Customers.UpdateCustomer;

namespace FleetControl.Tests.Helpers.Generators.Customers.Commands
{
    public class CustomerCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertCustomerCommand> _insertCommandFaker = new Faker<InsertCustomerCommand>()
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .RuleFor(c => c.Cnpj, f => f.Company.Cnpj())
            .RuleFor(c => c.Contact, f => f.Phone.PhoneNumber("(##) #####-####"))
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Name, f => f.Company.CompanyName());

        private readonly Faker<UpdateCustomerCommand> _updateCommandFaker = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.IdCustomer, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .RuleFor(c => c.Cnpj, f => f.Company.Cnpj())
            .RuleFor(c => c.Contact, f => f.Phone.PhoneNumber("(##) #####-####"))
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Name, f => f.Company.CompanyName());

        private readonly Faker<DeleteCustomerCommand> _deleteCommandFaker = new Faker<DeleteCustomerCommand>()
            .CustomInstantiator(f => new DeleteCustomerCommand(f.Random.Int(1, 100)));

        private readonly Faker<EnableCustomerCommand> _enableCommandFaker = new Faker<EnableCustomerCommand>()
            .CustomInstantiator(f => new EnableCustomerCommand(f.Random.Int(1, 100)));

        private readonly Faker<DisableCustomerCommand> _disableCommandFaker = new Faker<DisableCustomerCommand>()
            .CustomInstantiator(f => new DisableCustomerCommand(f.Random.Int(1, 100)));

        public CustomerCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker.Generate() },
                { CommandType.Update, _updateCommandFaker.Generate()},
                { CommandType.Delete, _deleteCommandFaker.Generate() },
                { CommandType.Enable, _enableCommandFaker.Generate() },
                { CommandType.Disable, _disableCommandFaker.Generate() }
            };
        }
    }
}
