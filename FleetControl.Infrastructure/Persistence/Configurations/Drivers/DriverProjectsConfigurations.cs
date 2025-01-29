using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Drivers
{
    public class DriverProjectsConfigurations : IEntityTypeConfiguration<DriverProjects>
    {
        public void Configure(EntityTypeBuilder<DriverProjects> builder)
        {
            builder.Property(dp => dp.Id).UseIdentityColumn();

            builder.HasKey(dp => new { dp.IdDriver, dp.IdProject });

            builder.HasOne(dp => dp.Driver).WithMany(d => d.DriverProjects).HasForeignKey(dp => dp.IdDriver).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(dp => dp.Project).WithMany().HasForeignKey(dp => dp.IdProject).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
