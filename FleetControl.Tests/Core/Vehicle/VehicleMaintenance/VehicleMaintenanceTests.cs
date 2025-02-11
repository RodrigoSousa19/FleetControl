using FleetControl.Core.Enums.Vehicle;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;

namespace FleetControl.Tests.Core.Vehicle.VehicleMaintenance
{
    public class VehicleMaintenanceTests
    {
        private readonly VehicleMaintenanceGenerator _generator = new VehicleMaintenanceGenerator();

        [Fact]
        public void VehicleMaintenanceCreated_SetAsDeleted_Success()
        {
            var maintenance = _generator.Generate();

            maintenance.SetAsDeleted();

            maintenance.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void VehicleMaintenanceCreated_SetInProgress_Success()
        {
            var maintenance = _generator.Generate();

            maintenance.SetInProgress();

            maintenance.Status.Should().Be(MaintenanceStatus.InProgress);
        }

        [Fact]
        public void VehicleMaintenanceCreated_Finish_StatusDontChange()
        {
            var maintenance = _generator.Generate();

            maintenance.FinishMaintenance();

            maintenance.Status.Should().Be(MaintenanceStatus.Pending);
        }

        [Fact]
        public void VehicleMaintenanceCreated_Cancel_Success()
        {
            var maintenance = _generator.Generate();

            maintenance.CancelMaintenance();

            maintenance.Status.Should().Be(MaintenanceStatus.Canceled);
        }

        [Fact]
        public void VehicleMaintenanceInProgress_Finish_Success()
        {
            var maintenance = _generator.Generate();

            maintenance.SetInProgress();

            maintenance.FinishMaintenance();

            maintenance.Status.Should().Be(MaintenanceStatus.Completed);
        }

        [Fact]
        public void VehicleMaintenanceInProgress_Cancel_StatusDontChange()
        {
            var maintenance = _generator.Generate();

            maintenance.SetInProgress();

            maintenance.CancelMaintenance();

            maintenance.Status.Should().Be(MaintenanceStatus.InProgress);
        }

        [Fact]
        public void VehicleMaintenanceFinished_SetInProgress_StatusDontChange()
        {
            var maintenance = _generator.Generate();

            maintenance.SetInProgress();

            maintenance.FinishMaintenance();

            maintenance.SetInProgress();

            maintenance.Status.Should().Be(MaintenanceStatus.Completed);
        }

        [Fact]
        public void VehicleMaintenanceFinished_Cancel_StatusDontChange()
        {
            var maintenance = _generator.Generate();

            maintenance.SetInProgress();

            maintenance.FinishMaintenance();

            maintenance.CancelMaintenance();

            maintenance.Status.Should().Be(MaintenanceStatus.Completed);
        }
    }
}
