using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailSalon.Domain.Common;

namespace NailSalon.Infrastructure.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        // Cấu hình Global Query Filter cho Soft Delete Module
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}