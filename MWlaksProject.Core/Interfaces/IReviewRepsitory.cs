using MWlaksProject.Core.DTOS.ReviewDto;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Interfaces
{
    public interface IReviewRepsitory
    {
        Task<Review>Add(CreateReviewDto dto,Guid user);
    }
}
