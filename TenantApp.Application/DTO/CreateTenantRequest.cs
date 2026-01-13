using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.DTO
{
    public class CreateTenantRequest
    {
        public string CompanyName { get; set; } = default!;
        public string AdminEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
