using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Application.DTO;
using TenantApp.Domain.Tenant;
using TenantApp.Domain.User;

namespace TenantApp.Application.Services
{
    public class TenantService
    {
        private readonly ITenantRepository _tenants;
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;

        public TenantService(
        ITenantRepository tenants,
        IUserRepository users,
        IUnitOfWork uow)
        {
            _tenants = tenants;
            _users = users;
            _uow = uow;
        }

        public async Task CreateTenantAsync(CreateTenantRequest request)
        {
            if (await _users.ExistsWithEmailAsync(request.AdminEmail))
                throw new ApplicationException("Email already exists");

            var tenant = new Tenant(request.CompanyName);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var admin = new User(
                tenant.Id,
                request.AdminEmail,
                passwordHash,
                isAdmin: true
            );

            await _tenants.AddAsync(tenant);
            await _users.AddAsync(admin);

            await _uow.SaveChangesAsync();
        }
    }
}
