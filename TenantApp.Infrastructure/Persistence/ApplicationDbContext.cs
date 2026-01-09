using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;
using TenantApp.Domain.Common;
using TenantApp.Domain.Customers;

namespace TenantApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantContext _tenantContext;

        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantContext tenantContext)
        : base(options)
        {
            _tenantContext = tenantContext;
        }

        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );


            ApplyGlobalTenantFilter(modelBuilder);
        }

        private void ApplyGlobalTenantFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext)
                        .GetMethod(nameof(SetTenantFilter),
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void SetTenantFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(e =>
                    _tenantContext.IsAvailable &&
                    e.TenantId == _tenantContext.TenantId);
        }
    }
}
