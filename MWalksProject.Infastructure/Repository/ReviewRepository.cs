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
        public async Task<Review> Add(Review review, string user)
        {
              user =  context.Entry(review).Property<string>("UserId").CurrentValue = user;

             await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
            return review;
        }
    }
}
