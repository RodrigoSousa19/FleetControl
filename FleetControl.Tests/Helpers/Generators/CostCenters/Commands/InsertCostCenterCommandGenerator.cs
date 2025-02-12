using Bogus;
using FleetControl.Application.Commands.CostCenters.DeleteCostCenter;
using FleetControl.Application.Commands.CostCenters.DisableCostCenter;
using FleetControl.Application.Commands.CostCenters.EnableCostCenter;
using FleetControl.Application.Commands.CostCenters.InsertCostCenter;
using FleetControl.Application.Commands.CostCenters.UpdateCostCenter;
using MediatR;

namespace FleetControl.Tests.Helpers.Generators
{
    public class CostCenterCommandsGenerator
    {
        public Dictionary<CommandType, object> Commands;

        private readonly Faker<InsertCostCenterCommand> _insertCommandFaker = new Faker<InsertCostCenterCommand>().RuleFor(c => c.Description, f => f.Lorem.Paragraph());
        private readonly Faker<UpdateCostCenterCommand> _updateCommandFaker = new Faker<UpdateCostCenterCommand>().RuleFor(c => c.IdCostCenter, f => f.Random.Int(1, 100)).RuleFor(c => c.Description, f => f.Lorem.Paragraph());
        private readonly Faker<DeleteCostCenterCommand> _deleteCommandFaker = new Faker<DeleteCostCenterCommand>().CustomInstantiator(f => new DeleteCostCenterCommand(f.Random.Int(1, 100)));
        private readonly Faker<EnableCostCenterCommand> _enableCommandFaker = new Faker<EnableCostCenterCommand>().CustomInstantiator(f => new EnableCostCenterCommand(f.Random.Int(1, 100)));
        private readonly Faker<DisableCostCenterCommand> _disableCommandFaker = new Faker<DisableCostCenterCommand>().CustomInstantiator(f => new DisableCostCenterCommand(f.Random.Int(1, 100)));

        public CostCenterCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>
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
