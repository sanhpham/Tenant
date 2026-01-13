using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantApp.Application.DTO;
using TenantApp.Application.Services;

namespace TenantApp.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _auth.LoginAsync(request);
        }
    }
}
