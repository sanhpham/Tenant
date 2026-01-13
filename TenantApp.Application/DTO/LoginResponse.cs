using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp.Application.DTO
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = default!;
    }
}
