using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantApp
{
    public class TenantConfigurationBuilder
    {
        public void BuildProjectAPIConfigurationBuilder()
        {
            TenantConfiguration.connectionStringSetting = BuilderConnectionStringSetting();
        }
        private ConnectionStringSetting BuilderConnectionStringSetting()
        {
            return new ConnectionStringSetting
            {
                DefaultConnection = GetRequiredConnectionString()
            };
        }
        private static string GetRequiredConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string conn = config.GetConnectionString("DefaultConnection") ?? "";

            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            return conn;
        }
    }
}
