using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FleetControl.Infrastructure.Persistence.Configurations.Reservations
{
    public class ReservationsConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();
            builder.Property(r => r.Observation).HasMaxLength(500).IsRequired();

            builder.HasOne(r => r.Customer).WithMany().HasForeignKey(r => r.IdCustomer).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Vehicle).WithMany().HasForeignKey(r => r.IdVehicle).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Driver).WithMany().HasForeignKey(r => r.IdDriver).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
