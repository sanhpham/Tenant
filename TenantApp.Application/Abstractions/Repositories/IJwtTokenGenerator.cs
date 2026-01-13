using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.Abstractions.Repositories
{
    public interface IJwtTokenGenerator
    {
        string Generate(Guid userId, Guid tenantId, string role);
    }
}
