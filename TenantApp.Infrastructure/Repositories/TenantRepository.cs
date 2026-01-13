using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Domain.Tenant;
using TenantApp.Infrastructure.Persistence;

namespace TenantApp.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationDbContext _context;

        public TenantRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tenant tenant)
        {
            await _context.Set<Tenant>().AddAsync(tenant);
        }
    }
}
