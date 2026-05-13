using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Configurations;

public class AppointmentConfiguration : BaseConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Notes).HasMaxLength(500);

        // Cấu hình Foreign Key (Ngăn lỗi cascade delete trong SQL Server)
        builder.HasOne(x => x.Customer)
               .WithMany(c => c.Appointments)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
               .WithMany(e => e.Appointments)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}