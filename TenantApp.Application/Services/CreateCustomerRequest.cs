using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.Services
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
