using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;
using TenantApp.Infrastructure.Tenancy;

namespace TenantApp.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            ITenantContext tenantContext = new TenantContext();

            optionsBuilder.UseSqlServer(
                "Server=(localDB)\\SanhPV;Database=CRM;User Id=sa;Password=123456789;TrustServerCertificate=True");

            return new ApplicationDbContext(optionsBuilder.Options, tenantContext);
        }
    }
}
