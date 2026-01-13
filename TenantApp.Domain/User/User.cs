using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApp.Domain.Common;

namespace TenantApp.Domain.User
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public bool IsAdmin { get; private set; }

        protected User() { }

        public User(Guid tenantId, string email, string passwordHash, bool isAdmin)
        {
            Id = Guid.NewGuid();
            TenantId = tenantId;
            Email = email;
            PasswordHash = passwordHash;
            IsAdmin = isAdmin;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
