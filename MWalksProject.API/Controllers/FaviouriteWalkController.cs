using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWlaksProject.Core.DTOS.FavioriteWalk;
using MWlaksProject.Core.DTOS.FavioriteWalkDTOS;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;

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
            var FaviouriteDto = mapper.Map<List<FaviouriteWalksDto>>(AllFaviourite);
           return Ok(FaviouriteDto);

        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddFaviouriteWalk (AddFaviouriteWalkDto dto)
        {
            var faviouriteWalke = mapper.Map<FaviouriteWalks>(dto);
            await unitOfWork.FavoriteWalks.Add(faviouriteWalke);
            var faviouriteWalkDto = mapper.Map<FaviouriteWalksDto>(faviouriteWalke);
            return StatusCode(201, new { message = "Faviourite Walk Added Sucessfully" });

        }

        [HttpDelete]
        [Route("RemoveFaviouriteAsync")]
        public async Task<IActionResult> RemoveFaviouriteAsync(Guid id)
        { 
            var  RemovedItem  = unitOfWork.FavoriteWalks.Remove(id);
            return NoContent();
        }


    }
}
