using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MWalksProject.Infastructure.Services
{
    public class TokenServices(UserManager<ApplicationUser>userManager,IOptions<JwtHelper>options) : ITokenServices
    { 
            public async Task<string> GenerateToken(ApplicationUser user)
            {
                var roles = await userManager.GetRolesAsync(user);
                var Userclaims = await userManager.GetClaimsAsync(user);
                var RoleClaims = new List<Claim>();
                foreach (var role in roles)
                {
                    RoleClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claim = new List<Claim>
            {
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
            }
                .Union(RoleClaims)
                .Union(Userclaims);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.key));
                var Credential = new SigningCredentials(securityKey, SecurityAlgorithms.Aes128CbcHmacSha256);
                var JwtSecurityToken = new JwtSecurityToken(
                claims: claim,
                    issuer: options.Value.Issuer,
                    audience: options.Value.Audience,
                    expires: DateTime.Now.AddMinutes(options.Value.lifetimeInMinutes),
                    signingCredentials: Credential
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
                return token;
            }
        }
    }
