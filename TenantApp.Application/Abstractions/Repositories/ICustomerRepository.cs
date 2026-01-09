using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.Customers;

namespace TenantApp.Application.Abstractions.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> ExistsWithEmailAsync(string email);
        Task AddAsync(Customer customer);
    }
}
