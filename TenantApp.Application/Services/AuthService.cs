using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Application.DTO;

namespace TenantApp.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _users;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(
        IUserRepository users,
        IJwtTokenGenerator jwt)
        {
            _users = users;
            _jwt = jwt;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _users.GetByEmailAsync(request.Email);

            if (user == null || !user.VerifyPassword(request.Password))
                throw new ApplicationException("Invalid credentials");

            var token = _jwt.Generate(
                user.Id,
                user.TenantId,
                user.IsAdmin ? "admin" : "user"
            );

            return new LoginResponse { AccessToken = token };
        }
    }
}
