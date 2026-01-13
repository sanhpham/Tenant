using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantApp.Application.DTO;
using TenantApp.Application.Services;

namespace TenantApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomersController(CustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            await _service.CreateCustomerAsync(request);
            return Ok();
        }
    }
}
