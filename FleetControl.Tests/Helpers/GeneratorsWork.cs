using FleetControl.Tests.Helpers.Generators;
using FleetControl.Tests.Helpers.Generators.Customers.Commands;
using FleetControl.Tests.Helpers.Generators.Drivers.Commands;
using FleetControl.Tests.Helpers.Generators.Projects.Commands;
using FleetControl.Tests.Helpers.Generators.Reservations.Commands;
using FleetControl.Tests.Helpers.Generators.Reservations.ReservationComment.Commands;
using FleetControl.Tests.Helpers.Generators.Users.Commands;
using FleetControl.Tests.Helpers.Generators.Vehicles.Commands;
using FleetControl.Tests.Helpers.Generators.Vehicles.VehicleMaintenance.Commands;

namespace FleetControl.Tests.Helpers
{
    public class GeneratorsWork
    {
        private CostCenterCommandsGenerator _costCenterCommandsGenerator;
        private CustomerCommandsGenerator _customerCommandsGenerator;
        private DriverCommandsGenerator _driverCommandsGenerator;
        private ProjectCommandsGenerator _projectCommandsGenerator;
        private ReservationCommandsGenerator _reservationCommandsGenerator;
        private ReservationCommentCommandsGenerator _reservationCommentCommandsGenerator;
        private UserCommandsGenerator _userCommandsGenerator;
        private VehicleCommandsGenerator _vehicleCommandsGenerator;
        private VehicleMaintenanceCommandsGenerator _vehicleMaintenanceCommandsGenerator;

        public CostCenterCommandsGenerator CostCenterCommandsGenerator
        {
            get
            {
                return _costCenterCommandsGenerator ?? (_costCenterCommandsGenerator = new CostCenterCommandsGenerator());
            }
        }

        public CustomerCommandsGenerator CustomerCommandsGenerator
        {
            get
            {
                return _customerCommandsGenerator ?? (_customerCommandsGenerator = new CustomerCommandsGenerator());
            }
        }

        public DriverCommandsGenerator DriverCommandsGenerator
        {
            get
            {
                return _driverCommandsGenerator ?? (_driverCommandsGenerator = new DriverCommandsGenerator());
            }
        }

        public ProjectCommandsGenerator ProjectCommandsGenerator
        {
            get
            {
                return _projectCommandsGenerator ?? (_projectCommandsGenerator = new ProjectCommandsGenerator());
            }
        }

        public ReservationCommandsGenerator ReservationCommandsGenerator
        {
            get
            {
                return _reservationCommandsGenerator ?? (_reservationCommandsGenerator = new ReservationCommandsGenerator());
            }
        }

        public ReservationCommentCommandsGenerator ReservationCommentCommandsGenerator
        {
            get
            {
                return _reservationCommentCommandsGenerator ?? (_reservationCommentCommandsGenerator = new ReservationCommentCommandsGenerator());
            }
        }

        public UserCommandsGenerator UserCommandsGenerator
        {
            get
            {
                return _userCommandsGenerator ?? (_userCommandsGenerator = new UserCommandsGenerator());
            }
        }
        public VehicleCommandsGenerator VehicleCommandsGenerator
        {
            get
            {
                return _vehicleCommandsGenerator ?? (_vehicleCommandsGenerator = new VehicleCommandsGenerator());
            }
        }

        public VehicleMaintenanceCommandsGenerator VehicleMaintenanceCommandsGenerator
        {
            get
            {
                return _vehicleMaintenanceCommandsGenerator ?? (_vehicleMaintenanceCommandsGenerator = new VehicleMaintenanceCommandsGenerator());
            }
        }
    }
}
