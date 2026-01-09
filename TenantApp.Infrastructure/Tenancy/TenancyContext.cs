using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;

namespace TenantApp.Infrastructure.Tenancy
{
    public class TenantContext : ITenantContext
    {
        public Guid TenantId { get; private set; }
        public bool IsAvailable => TenantId != Guid.Empty;

        public void SetTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
