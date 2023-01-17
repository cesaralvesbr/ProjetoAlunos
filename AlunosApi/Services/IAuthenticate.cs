using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public interface IAuthenticate
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> AuthenticateUser(string email, string password);
        Task<List<IdentityUser>> GetUsers();
        Task Logout();
    }
}
