using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Vehicles
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Brand).HasMaxLength(20).IsRequired();
            builder.Property(v => v.Model).HasMaxLength(100).IsRequired();
            builder.Property(v => v.FuelType).HasMaxLength(50).IsRequired();
            builder.Property(v => v.LicensePlate).HasMaxLength(7).IsRequired();
            builder.Property(v => v.Color).HasMaxLength(20).IsRequired();
            builder.Property(v => v.MileAge).IsRequired();

            builder.HasOne(v => v.Project).WithMany().HasForeignKey(v => v.IdProject);
        }
    }
}
