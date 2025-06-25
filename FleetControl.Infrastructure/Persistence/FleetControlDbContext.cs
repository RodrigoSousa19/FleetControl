using FleetControl.Core.Entities;
using FleetControl.Core.Entities.Email;
using Microsoft.EntityFrameworkCore;

namespace FleetControl.Infrastructure.Persistence
{
    public class FleetControlDbContext : DbContext
    {
        public FleetControlDbContext(DbContextOptions<FleetControlDbContext> options) : base(options)
        {

        }

        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationComment> ReservationComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenance { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FleetControlDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
