using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.DTO
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
