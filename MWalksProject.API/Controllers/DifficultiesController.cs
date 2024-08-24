using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWalksProject.Infastructure.UnitOfWork;
using MWlaksProject.Core.DTOS.DifficultiesDto;
using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.IUnitOfWork;
using System.Diagnostics.Metrics;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController(IUnitOfWork unitOfWork , IMapper mapper) : ControllerBase
    {
       /* [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(QuaryObject quary)
        {
            var Difficulties = await unitOfWork.Difficulty.GetAllAsync(quary);
            return Ok (Difficulties);
            
        }
        [HttpGet]
        [Route("GetByIdAsync/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var Difficult = await unitOfWork.Difficulty.GetByIdAsync(id);
            return Ok (Difficult);
             
        }

        [HttpPut]
        [Route("updateAsync")]
        public async Task<IActionResult> updateAsync(DifficultDTO entity, Guid id)
        {

             
        }
        [HttpPost]
        [Route("CreateAsync")]
        public async Task<IActionResult> CreateAsync(DifficultDTO entity)
        {
            var walk = await unitOfWork.Difficulty.CreateAsync(entity);
            var walkMapper = mapper.Map<AddWalkDto>(walk);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = walk.Id }, new { message = "WalkCreated successfuly" });

        }
        [HttpDelete]
        [Route("deleteAsync")]
        public async Task<IActionResult> deleteAsync(Guid id)
        {
             

        }
*/
    }
}
