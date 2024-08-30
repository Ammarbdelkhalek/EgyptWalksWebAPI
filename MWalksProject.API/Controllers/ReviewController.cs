using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MWlaksProject.Core.DTOS.ReviewDto;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;

namespace MWalksProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IUnitOfWork unitOfWork , IMapper mapper) : ControllerBase
    {
        [HttpPost]
        [Route("CreateReview")]
        public async Task<IActionResult> CreateAsync(CreateReviewDto dto)
        {
            var mappedValues = mapper.Map<Review>(dto);
            var review = await unitOfWork.Reviews.Add(mappedValues, dto.ApplicationUserId);
            return Ok(mappedValues);

        }
    }
}
