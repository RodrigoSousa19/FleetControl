using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FleetControl.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly FleetControlDbContext _context;

        public UnitOfWork(IGenericRepository<CostCenter> costCenterRepository,
                          IGenericRepository<Customer> customerRepository,
                          IGenericRepository<Driver> driverRepository,
                          IGenericRepository<DriverProjects> driverProjectsRepository,
                          IGenericRepository<Project> projectRepository,
                          IGenericRepository<Reservation> reservationRepository,
                          IGenericRepository<ReservationComment> reservationCommentRepository,
                          IUserRepository userRepository,
                          IGenericRepository<Vehicle> vehicleRepository,
                          IGenericRepository<VehicleMaintenance> vehicleMaintenanceRepository,
                          FleetControlDbContext context)
        {
            CostCenterRepository = costCenterRepository;
            CustomerRepository = customerRepository;
            DriverRepository = driverRepository;
            DriverProjectsRepository = driverProjectsRepository;
            ProjectRepository = projectRepository;
            ReservationRepository = reservationRepository;
            ReservationCommentRepository = reservationCommentRepository;
            UserRepository = userRepository;
            VehicleRepository = vehicleRepository;
            VehicleMaintenanceRepository = vehicleMaintenanceRepository;
            _context = context;
        }

        public IGenericRepository<CostCenter> CostCenterRepository { get; }

        public IGenericRepository<Customer> CustomerRepository { get; }

        public IGenericRepository<Driver> DriverRepository { get; }

        public IGenericRepository<DriverProjects> DriverProjectsRepository { get; }

        public IGenericRepository<Project> ProjectRepository { get; }

        public IGenericRepository<Reservation> ReservationRepository { get; }

        public IGenericRepository<ReservationComment> ReservationCommentRepository { get; }

        public IUserRepository UserRepository { get; }

        public IGenericRepository<Vehicle> VehicleRepository { get; }

        public IGenericRepository<VehicleMaintenance> VehicleMaintenanceRepository { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
