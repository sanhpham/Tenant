using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;
using TenantApp.Domain.Common;
using TenantApp.Domain.Customers;
using TenantApp.Domain.Tenant;
using TenantApp.Domain.User;

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

        public DbSet<Tenant> Tenant => Set<Tenant>();  

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );

            if(_tenantContext.IsAvailable)
            {
                ApplyGlobalTenantFilter(modelBuilder);
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(
        //        "Server=(localDB)\\SanhPV;Database=CRM;User Id=sa;Password=123456789;TrustServerCertificate=True");
        //}

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
                    !_tenantContext.IsAvailable ||
                    e.TenantId == _tenantContext.TenantId);
        }
    }
}
