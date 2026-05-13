using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Configurations;

public class NailServiceConfiguration : BaseConfiguration<NailService>
{
    public override void Configure(EntityTypeBuilder<NailService> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.ServiceName).IsRequired().HasMaxLength(200);

        // Bắt buộc cấu hình chính xác cho kiểu decimal (18 chữ số tổng cộng, 2 chữ số phần thập phân)
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
    }
}