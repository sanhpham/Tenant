using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.Abstractions
{
    public interface ITenantContext
    {
        Guid TenantId { get; }
        bool IsAvailable { get; }
    }
}
