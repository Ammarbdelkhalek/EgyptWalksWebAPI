using MWlaksProject.Core.DTOS.AccountDTO;
using MWlaksProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Interfaces
{
    public interface IauthRepository
    {
        Task<AuthModel> LoginAsync(LoginDto loginDto);
        Task<AuthModel> RegisterAsync(RegisterDto registerDto);
        Task LogOut();
    }
}
