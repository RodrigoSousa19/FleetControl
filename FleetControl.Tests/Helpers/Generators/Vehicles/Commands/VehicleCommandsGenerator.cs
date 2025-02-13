using Bogus;
using FleetControl.Application.Commands.Vehicles.DeleteVehicle;
using FleetControl.Application.Commands.Vehicles.DisableVehicle;
using FleetControl.Application.Commands.Vehicles.EnableVehicle;
using FleetControl.Application.Commands.Vehicles.InsertVehicle;
using FleetControl.Application.Commands.Vehicles.SetVehicleAvailable;
using FleetControl.Application.Commands.Vehicles.SetVehicleReserved;
using FleetControl.Application.Commands.Vehicles.UpdateVehicle;
using FleetControl.Tests.FakerExtensions;

namespace FleetControl.Tests.Helpers.Generators.Vehicles.Commands
{
    public class VehicleCommandsGenerator : CommandsGeneratorBase
    {
        private readonly Faker<InsertVehicleCommand> _insertCommandFaker = new Faker<InsertVehicleCommand>()
            .RuleFor(v => v.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(v => v.Model, f => f.Vehicle.Model())
            .RuleFor(v => v.Color, f => f.Commerce.Color())
            .RuleFor(v => v.FuelType, f => f.Vehicle.Fuel())
            .RuleFor(v => v.MileAge, f => f.Random.Int(1, 100000))
            .RuleFor(v => v.LicensePlate, f => f.Vehicle.BrazilLicensePlate());

        private readonly Faker<UpdateVehicleCommand> _updateCommandFaker = new Faker<UpdateVehicleCommand>()
            .RuleFor(v => v.IdVehicle, f => f.Random.Int(1, 100))
            .RuleFor(v => v.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(v => v.Model, f => f.Vehicle.Model())
            .RuleFor(v => v.Color, f => f.Commerce.Color())
            .RuleFor(v => v.FuelType, f => f.Vehicle.Fuel())
            .RuleFor(v => v.MileAge, f => f.Random.Int(1, 100000))
            .RuleFor(v => v.LicensePlate, f => f.Vehicle.BrazilLicensePlate());

        private readonly Faker<DeleteVehicleCommand> _deleteCommandFaker = new Faker<DeleteVehicleCommand>()
            .CustomInstantiator(f => new DeleteVehicleCommand(f.Random.Int(1, 100)));

        private readonly Faker<EnableVehicleCommand> _enableCommandFaker = new Faker<EnableVehicleCommand>()
            .CustomInstantiator(f => new EnableVehicleCommand(f.Random.Int(1, 100)));

        private readonly Faker<DisableVehicleCommand> _disableCommandFaker = new Faker<DisableVehicleCommand>()
            .CustomInstantiator(f => new DisableVehicleCommand(f.Random.Int(1, 100)));

        private readonly Faker<SetVehicleReservedCommand> _reserveCommandFaker = new Faker<SetVehicleReservedCommand>()
            .RuleFor(v => v.IdVehicle, f => f.Random.Int(1, 100))
            .RuleFor(v => v.IdProject, f => f.Random.Int(1, 100));

        private readonly Faker<SetVehicleAvailableCommand> _availableCommandFaker = new Faker<SetVehicleAvailableCommand>()
            .CustomInstantiator(f => new SetVehicleAvailableCommand(f.Random.Int(1, 100)));

        public VehicleCommandsGenerator()
        {
            Commands = new Dictionary<CommandType, object>()
            {
                { CommandType.Insert, _insertCommandFaker },
                { CommandType.Update, _updateCommandFaker },
                { CommandType.Delete, _deleteCommandFaker },
                { CommandType.Enable, _enableCommandFaker },
                { CommandType.Disable, _disableCommandFaker},
                { CommandType.SetReserved, _reserveCommandFaker },
                { CommandType.SetAvailable, _availableCommandFaker }
            };
        }
    }
}
