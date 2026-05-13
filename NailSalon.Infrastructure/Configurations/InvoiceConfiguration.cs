using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Configurations;

public class InvoiceConfiguration : BaseConfiguration<Invoice>
{
    public override void Configure(EntityTypeBuilder<Invoice> builder)
    {
        base.Configure(builder);

        // Cấu hình mã hóa đơn không được trùng và có độ dài tối đa
        builder.Property(x => x.InvoiceCode).IsRequired().HasMaxLength(50);
        builder.HasIndex(x => x.InvoiceCode).IsUnique();

        // Tổng tiền hóa đơn
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Customer)
               .WithMany(c => c.Invoices)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
               .WithMany(e => e.Invoices)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}