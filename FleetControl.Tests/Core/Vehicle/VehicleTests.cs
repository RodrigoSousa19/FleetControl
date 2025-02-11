using FleetControl.Core.Enums.Vehicle;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;

namespace FleetControl.Tests.Core.Vehicle
{
    public class VehicleTests
    {
        private readonly VehicleGenerator _generator = new VehicleGenerator();

        [Fact]
        public void VehicleIsCreated_SetDeleted_Success()
        {
            var vehicle = _generator.Generate();

            vehicle.SetAsDeleted();

            vehicle.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void VehicleIsAvailable_SendToMaintenance_Success()
        {
            var vehicle = _generator.Generate();

            vehicle.SendToMaintenance();

            vehicle.Status.Should().Be(VehicleStatus.InMaintenance);
            vehicle.LastMaintenance.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void VehicleIsAvailable_SetReserved_Success()
        {
            var vehicle = _generator.Generate();

            vehicle.SetReserved(Arg.Any<int>());

            vehicle.IdProject.Should().NotBeNull();
            vehicle.Status.Should().Be(VehicleStatus.Reserved);
        }

        [Fact]
        public void VehicleIsInMaintenance_SetReserved_StatusDontChange()
        {
            var vehicle = _generator.Generate();

            vehicle.SendToMaintenance();

            vehicle.SetReserved(Arg.Any<int>());

            vehicle.IdProject.Should().BeNull();
            vehicle.Status.Should().Be(VehicleStatus.InMaintenance);
        }

        [Fact]
        public void VehicleIsInMaintenance_SetAvailable_Success()
        {
            var vehicle = _generator.Generate();

            vehicle.SendToMaintenance();

            vehicle.SetAvailable();

            vehicle.Status.Should().Be(VehicleStatus.Available);
        }

        [Fact]
        public void VehicleIsReserved_SendToMaintenance_StatusDontChange()
        {
            var vehicle = _generator.Generate();

            vehicle.SetReserved(Arg.Any<int>());

            vehicle.SendToMaintenance();

            vehicle.Status.Should().Be(VehicleStatus.Reserved);
            vehicle.IdProject.Should().NotBeNull();
        }


        [Fact]
        public void VehicleIsReserved_SetAvailable_Success()
        {
            var vehicle = _generator.Generate();

            vehicle.SetAvailable();

            vehicle.Status.Should().Be(VehicleStatus.Available);
            vehicle.IdProject.Should().BeNull();
        }
    }
}
