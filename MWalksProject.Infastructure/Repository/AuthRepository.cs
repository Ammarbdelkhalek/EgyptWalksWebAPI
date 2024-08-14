using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MWlaksProject.Core.DTOS.AccountDTO;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Services;
using System.Security.Principal;


namespace MWalksProject.Infastructure.Repository
{
    public class AuthRepository(UserManager<ApplicationUser> userManager,ITokenServices tokenServices, SignInManager<ApplicationUser> SiginManager) : IauthRepository
    {
        public async Task<AuthModel> LoginAsync(LoginDto loginDto)
        {
            var User = await userManager.FindByNameAsync(loginDto.UserName);
             

            if (User == null)
            {
                return new AuthModel { Message = "userName not found" };
            }

            var passwordIsCorrect = await userManager.CheckPasswordAsync(User, loginDto.Password);

            if (!passwordIsCorrect)
            {
                return new AuthModel { Message = "Invalid password" };
            }
            var token = await tokenServices.GenerateToken(User);
           // await userManager.GetRolesAsync(User);
            return new AuthModel
            {
                Message = "you Logged in Sucessfully",
                IsAuthenticated = true,
                Email = User.Email,
                //Roles = new List<string> { " User" },
                UserName = User.UserName,
                Token = token,
            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto registerDto)
        {
            if (await userManager.FindByEmailAsync(registerDto.Email) is not null)
            {
                return new AuthModel { Message = "Email is already Exist" };
            }
            if(await userManager.FindByNameAsync(registerDto.UserName) is not null)
            {
                return new AuthModel { Message = "Username is already Exist" };
            }
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName
            };

            var Result = await userManager.CreateAsync(user, registerDto.Password);
            if (!Result.Succeeded)
            {
                var error = string.Empty;
                foreach (var item in Result.Errors)
                {
                    error += item.Description;
                }
                return new AuthModel { Message = error };
            }

            var token = await tokenServices.GenerateToken(user);
            //var Roles = await userManager.AddToRoleAsync(user, "User");

            return new AuthModel
            {
                Message = " Registered Sucessfully",
                IsAuthenticated = true,
                Email = user.Email,
                UserName = user.UserName,
               // Roles = new List<string> { " User" },
                Token = token,
            };
        }
        public async Task LogOut()
        {
           await SiginManager.SignOutAsync();
          
        }
    }

}
