using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantApp.Application.DTO;
using TenantApp.Application.Services;

namespace TenantApp.API.Controllers
{
    [ApiController]
    [Route("api/tenants")]
    public class TenantsController : ControllerBase
    {
        private readonly TenantService _service;

        public TenantsController(TenantService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantRequest request)
        {
            await _service.CreateTenantAsync(request);
            return Ok();
        }
    }
}
