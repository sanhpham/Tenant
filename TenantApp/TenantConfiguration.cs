using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp
{
    public class TenantConfiguration
    {
        public static ConnectionStringSetting? connectionStringSetting { get; set; }
    }

    public class ConnectionStringSetting
    {
        public string DefaultConnection { get; set; } = "";
    }
}
