using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWalksProject.Infastructure.UnitOfWork;
using MWlaksProject.Core.DTOS.DifficultiesDto;
using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using System.Diagnostics.Metrics;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController(IUnitOfWork unitOfWork , IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Difficulties = await unitOfWork.Difficulty.GetAllAsync();
            var DifficultiesDto = mapper.Map<IEnumerable<DifficultDTO>>(Difficulties);
            return Ok(Difficulties);
        }
        [HttpGet]
        [Route("GetByIdAsync{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var Difficulty = await unitOfWork.Difficulty.GetByIdAsync(id);
            var difficultyDto = mapper.Map<DifficultDTO>(Difficulty);
            return Ok(difficultyDto);
        }

        [HttpPut]
        [Route("updateAsync")]
        public async Task<IActionResult> updateAsync(DifficultDTO dto, Guid id)
        {
            var Difficulty = mapper.Map<Difficulty>(dto);
            var updateDifficulty = await unitOfWork.Difficulty.updateAsync(Difficulty, id);
            var mappedDifficulty = mapper.Map<DifficultDTO>(updateDifficulty);
 
            return Ok(mappedDifficulty);
        }
        [HttpPost]
        [Route("CreateAsync")]
        public async Task<IActionResult> CreateAsync(DifficultDTO dto)
        {
            var Diffculty = mapper.Map<Difficulty>(dto);
            var createdDifficulty = await unitOfWork.Difficulty.CreateAsync(Diffculty);
            var DifficultyDto = mapper.Map<DifficultDTO>(createdDifficulty);
             
            return  StatusCode(201, DifficultyDto);
            //CreatedAtAction(nameof(GetByIdAsync), new { id = Diffculty.Id }, new { message = "Walk Created successfuly" });
        }
        [HttpDelete]
        [Route("deleteAsync")]
        public async Task<IActionResult> deleteAsync(Guid id)
        {
            var deletedDifficulty = await unitOfWork.Difficulty.deleteAsync(id);
            return NoContent();
        }
    }
}
