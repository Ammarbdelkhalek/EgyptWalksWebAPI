using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWlaksProject.Core.DTOS.WalksDTO;
using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController(IUnitOfWork unitOfWork , IMapper mapper) : ControllerBase
    {

        [HttpGet]
        [Route("GetALLWalksAsync")]
        public async Task<IActionResult> GetAllWalksAsync(
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? ascending,
            int page = 1,
            int limit = 5
            )
        {
            var (data, pagination) = await unitOfWork.Walks.GetAllWalksAsync(filterOn, filterQuery, sortBy, ascending ?? true, page, limit);
            var mappedData = mapper.Map<IEnumerable<WalksDto>>(data);
            var response = new
            {
                data = mappedData,
                pagination = pagination
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var walk = await unitOfWork.Walks.GetByIdAsync(id);
            if(walk == null)
            {
                return NotFound("item not found");
            }
            return Ok(walk);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync(AddWalkDto dto)
        {
            var walk = await unitOfWork.Walks.CreateAsync(dto);
            var walkMapper = mapper.Map<AddWalkDto>(walk);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = walk.Id }, new { message = "Walk Created successfuly" });

        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult>DeleteAsync(Guid id)
        {
            var walk = await unitOfWork.Walks.GetByIdAsync(id);
            return NoContent();
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult>UpdateAsync(UpdateWalksDto dto,Guid id)
        {
            var updatedWalk = await unitOfWork.Walks.updateAsync(dto, id);
            var response = mapper.Map<UpdateWalksDto>(updatedWalk);
            return Ok(new { message =  "updated Sucessfuly",Response = response});
        }

    }
}
