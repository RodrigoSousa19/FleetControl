using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;

namespace FleetControl.Infrastructure.Persistence.Repositories
{
    public interface
        IUnitOfWork : IDisposable
    {
        IGenericRepository<CostCenter> CostCenterRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Driver> DriverRepository { get; }
        IGenericRepository<DriverProjects> DriverProjectsRepository { get; }
        IGenericRepository<Project> ProjectRepository { get; }
        IGenericRepository<Reservation> ReservationRepository { get; }
        IGenericRepository<ReservationComment> ReservationCommentRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Vehicle> VehicleRepository { get; }
        IGenericRepository<VehicleMaintenance> VehicleMaintenanceRepository { get; }

        Task SaveChangesAsync();
        Task BeginTransactin();
        Task CommitAsync();
    }
}
