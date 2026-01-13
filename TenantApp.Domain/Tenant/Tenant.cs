using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.Common;

namespace TenantApp.Domain.Tenant
{
    public class Tenant : BaseEntity
    {
        public string Name { get; private set; } = default!;

        protected Tenant() { }

        public Tenant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
