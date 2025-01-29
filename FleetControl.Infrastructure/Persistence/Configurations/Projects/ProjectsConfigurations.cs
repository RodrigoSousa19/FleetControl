using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Projects
{
    public class ProjectsConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.CostCenter).WithMany().HasForeignKey(p => p.IdCostCenter).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Customer).WithMany().HasForeignKey(p => p.IdCustomer).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
