using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Interfaces
{
    public interface IWalkRepository 
    {
        Task<(IEnumerable<Walks> Data, PaginationMetaData Pagination)> GetAllWalksAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool ascending = true,
            int page = 1,
            int limit = 5);
        Task<IEnumerable<Walks>> GetAllAsync(QuaryObject quaryObject);

        Task<Walks> GetByIdAsync(Guid id);
        Task<Walks> CreateAsync(AddWalkDto dto);
        Task<Walks> updateAsync(UpdateWalksDto dto , Guid id);
        Task<Walks> deleteAsync(Guid id);

    }
}
