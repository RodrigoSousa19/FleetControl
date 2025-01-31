using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Drivers
{
    public class DriversConfigurations : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DocumentNumber).HasMaxLength(14).IsRequired();
            builder.Property(d => d.DocumentType).HasMaxLength(20).IsRequired();

            builder.HasOne(d => d.User).WithMany().HasForeignKey(d => d.IdUser).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
