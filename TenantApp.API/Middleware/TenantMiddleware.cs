using TenantApp.Infrastructure.Tenancy;
using TenantApp.Application.Abstractions;

namespace TenantApp.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async void Invoke(HttpContext context , ITenantContext tenantContext)
        {
            // Lấy tenant từ JWT
            var tenantClaim = context.User?.Claims
                .FirstOrDefault(c => c.Type == "tenant_id");

            if (tenantClaim == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Tenant not found");
                return;
            }

            if (!Guid.TryParse(tenantClaim.Value, out var tenantId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid tenant");
                return;
            }

            // Set tenant cho request
            ((TenantContext)tenantContext).SetTenant(tenantId);

            await _next(context);
        }
    }
}
