using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.User;

namespace TenantApp.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<bool> ExistsWithEmailAsync(string email);

        Task<User?> GetByEmailAsync(string email);
    }
}
