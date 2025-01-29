using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Vehicles
{
    public class VehicleMaintenanceConfigurations : IEntityTypeConfiguration<VehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
        {
            builder.HasKey(vm => vm.Id);

            builder.Property(vm => vm.Description).HasMaxLength(1000).IsRequired();
            builder.Property(vm => vm.TotalCost).IsRequired();
            builder.Property(vm => vm.StartDate).IsRequired();
            builder.Property(vm => vm.EndDate).IsRequired();

            builder.HasOne(v => v.Vehicle).WithMany().HasForeignKey(vm => vm.IdVehicle).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
