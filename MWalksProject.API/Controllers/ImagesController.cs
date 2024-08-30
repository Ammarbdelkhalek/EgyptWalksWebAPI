using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWlaksProject.Core.CustomValidationAttribute;
using MWlaksProject.Core.DTOS.ImagesDtos;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using System.Runtime.InteropServices;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IUnitOfWork unitOfWork ,IMapper mapper) : ControllerBase
    {
        [HttpPost]
        [Route("UploadImages")]
        [FileValidator("Jpg, png",-1)]
        public async Task<IActionResult>uploadImage([FromForm]ImageDto dto)
        {
            var ImageUploaded = await unitOfWork.Images.UploadImageAsync(dto);
            if (ImageUploaded != null)
            {

                return Ok(ImageUploaded);
            }
            return BadRequest(new { message = "SomeThing Wrong Happen" });
        }
        [HttpDelete]
        [Route("DeleteImages")]
        public async Task <IActionResult> DeleteAync(Guid id)
        {
            var IsDeleted = await unitOfWork.Images.DeleteImage(id);
            if (!IsDeleted)
            {
                return BadRequest(new { message = "Deleted Failed SomeThing Wrong happen" });
            }
            return NoContent();

        }
    }
}
