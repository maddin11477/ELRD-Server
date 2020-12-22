using ELRDServerAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, string username);

        Task<AuthenticationResult> LoginAsync(string email, string password, string username);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshtoken);
    }
}
