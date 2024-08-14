using AutoMapper;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.DTOS.ReviewDto;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWalksProject.Infastructure.Repository
{
    public class ReviewRepository(ApplicationDbContext context , IMapper mapper) : IReviewRepsitory
    {
        public async Task<Review> Add(CreateReviewDto Dto, Guid user)
        {
              user =  context.Entry(Dto).Property<Guid>("ApplicationUserId").CurrentValue = user;

            var mappedReviews = mapper.Map<Review>(Dto);
             await context.Reviews.AddAsync(mappedReviews);
            await context.SaveChangesAsync();
            return mappedReviews;
        }
    }
}
