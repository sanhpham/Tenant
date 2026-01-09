using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.Common;

namespace TenantApp.Domain.Customers
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        protected Customer() { }

        public Customer(Guid tenantId, string name, string email)
        {
            Id = Guid.NewGuid();
            TenantId = tenantId;
            Name = name;
            Email = email;
        }

        public static Customer Create(Guid tenantId, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Customer name is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required");

            return new Customer(tenantId, name, email);
        }
    }
}
