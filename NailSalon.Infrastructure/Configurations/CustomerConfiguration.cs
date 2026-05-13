using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Entities;

namespace NailSalon.Infrastructure.Configurations;

public class CustomerConfiguration : BaseConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder); // Kế thừa Id và IsDeleted

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(x => x.Email).HasMaxLength(100);

        // Đánh Index cho Số điện thoại để tìm kiếm (Search Module) cực nhanh
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
    }
}