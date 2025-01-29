using FleetControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetControl.Infrastructure.Persistence.Configurations.Reservations
{
    public class ReservationCommentsConfigurations : IEntityTypeConfiguration<ReservationComment>
    {
        public void Configure(EntityTypeBuilder<ReservationComment> builder)
        {
            builder.HasKey(rc => rc.Id);

            builder.HasOne(rc => rc.Reservation).WithMany(r => r.ReservationComments).HasForeignKey(rc => rc.IdReservation).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rc => rc.User).WithMany(u => u.Comments).HasForeignKey(rc => rc.IdUser).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
