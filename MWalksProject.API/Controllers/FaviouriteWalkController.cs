using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWlaksProject.Core.IUnitOfWork;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaviouriteWalkController (IUnitOfWork unitOfWork,IMapper mapper): ControllerBase
    {
        [HttpGet]
        [Route("GetAllFavourite")]
        public async Task<IActionResult> GetAllFavourite()
        {
            var AllFaviourite = await unitOfWork.FavoriteWalks.GetAllFaviouriteWalks();
           return Ok(AllFaviourite);

        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> RemoveFaviouriteAsync(Guid id)
        { 
            var  RemovedItem  = unitOfWork.FavoriteWalks.Remove(id);
            return NoContent();
        }


    }
}
