using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWalksProject.Infastructure.Services;
using MWlaksProject.Core.DTOS.AccountDTO;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Services;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IauthRepository authRepo) : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
       public  async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var Result = await authRepo.LoginAsync(loginDto);
            if(!Result.IsAuthenticated)
            {
                return BadRequest(Result.Message);

            }
            return Ok(Result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await authRepo.RegisterAsync(registerDto);
            if(!Result.IsAuthenticated)
            {
                return BadRequest(Result.Message);
            }
            return Ok(Result);
        }

        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await authRepo.LogOut();
           return Ok(new { message = "you Looged in sucessfuly"});
        }

    }
}
