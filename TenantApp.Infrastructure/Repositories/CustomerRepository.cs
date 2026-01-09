using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Infrastructure.Persistence;
using TenantApp.Application.Abstractions.Repositories;
using TenantApp.Domain.Customers;

namespace TenantApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsWithEmailAsync(string email)
        {
            return await _context.Customers
                .AnyAsync(x => x.Email == email);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }
    }
}
