using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWalksProject.Infastructure.UnitOfWork;
using MWlaksProject.Core.DTOS.RegionDto;
using MWlaksProject.Core.DTOS.RegionDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Utilities;
using System.Drawing;
using Region = MWlaksProject.Core.Models.Region;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionControllers(IUnitOfWork unitOfWork,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [Route("GetALLRegions")]
        public async Task<IActionResult> GetAllAsync([FromQuery]QuaryObject quaryObject)
        {
            var Regions = await unitOfWork.Region.GetAllAsync();
            var RegionDto = mapper.Map<IEnumerable<RegionDto>>(Regions);
            return Ok(RegionDto);
        }
        [HttpGet]
        [Route("GetRegionById {id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var region = await unitOfWork.Region.GetByIdAsync(id);
            var RegionDto = mapper.Map<RegionDto>(region);
            return Ok(RegionDto);
        }
        [HttpPost]
        [Route("createRegionAsync")]
        public async Task<IActionResult>CreateAsync([FromForm]AddRegionDto dto )
        {
            var region = mapper.Map<Region>(dto);
            region.RegionImageUrl = dto.RegionImageUrl != null ? await ImageConverters.ToByteArray(dto.RegionImageUrl) : null;
            await unitOfWork.Region.CreateAsync(region);
            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);


        }
        [HttpPut]
        [Route("UpdateRegion")]
        public async Task<IActionResult>UpdateAsync(AddRegionDto dto ,[FromQuery]Guid id)
        {
            var region = mapper.Map<Region>(dto);
            var UpdatingRegion = await unitOfWork.Region.updateAsync(region, id);
            var regionDto = mapper.Map<AddRegionDto>(UpdatingRegion);
            return Ok(regionDto);
        }
        [HttpDelete]
        [Route("Delete Region")]
        public async Task<IActionResult>DeleteAsync(Guid id)
        {
            await unitOfWork.Region.deleteAsync(id);
            return NoContent();
        }
    }
}
