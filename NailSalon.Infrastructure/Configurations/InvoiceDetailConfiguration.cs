using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Configurations;

public class InvoiceDetailConfiguration : BaseConfiguration<InvoiceDetail>
{
    public override void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        base.Configure(builder);

        // Bắt buộc cấu hình decimal cho tiền tệ
        builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");

        // Xóa Invoice -> Xóa luôn InvoiceDetail
        builder.HasOne(x => x.Invoice)
               .WithMany(i => i.InvoiceDetails)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        // Ngăn chặn việc xóa Dịch vụ Nail nếu đã có khách thanh toán dịch vụ này
        builder.HasOne(x => x.NailService)
               .WithMany(s => s.InvoiceDetails)
               .HasForeignKey(x => x.NailServiceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}