using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class Authenticate : IAuthenticate
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userInManager;

        public Authenticate(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userInManager)
        {
            _signInManager = signInManager;
            _userInManager = userInManager;
        }


        public async Task<bool> AuthenticateUser(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var appUser = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userInManager.CreateAsync(appUser, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
            }

            return result.Succeeded;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
