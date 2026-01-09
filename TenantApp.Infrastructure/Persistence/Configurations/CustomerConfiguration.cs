using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantApp.Domain.Customers;

namespace TenantApp.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            // MULTI-TENANT UNIQUE INDEX
            builder.HasIndex(x => new { x.TenantId, x.Email })
                .IsUnique();

            // PERFORMANCE INDEX
            builder.HasIndex(x => x.TenantId);
        }
    }
}
