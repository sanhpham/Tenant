using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Application.Abstractions;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Domain.Customers;

namespace TenantApp.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customers;
        private readonly IUnitOfWork _uow;
        private readonly ITenantContext _tenantContext;

        public CustomerService(
            ICustomerRepository customers,
            IUnitOfWork uow,
            ITenantContext tenantContext)
        {
            _customers = customers;
            _uow = uow;
            _tenantContext = tenantContext;
        }

        public async Task CreateCustomerAsync(CreateCustomerRequest request)
        {
            if (!_tenantContext.IsAvailable)
                throw new ApplicationException("Tenant not resolved");

            if (await _customers.ExistsWithEmailAsync(request.Email))
                throw new ApplicationException("Customer already exists");

            var customer = Customer.Create(
                _tenantContext.TenantId,
                request.Name,
                request.Email
            );

            await _customers.AddAsync(customer);
            await _uow.SaveChangesAsync();
        }
    }
}
