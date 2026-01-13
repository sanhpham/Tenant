using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.Tenant;

namespace TenantApp.Application.Abstractions.Repositories
{
    public interface ITenantRepository
    {
        Task AddAsync(Tenant tenant);
    }
}
