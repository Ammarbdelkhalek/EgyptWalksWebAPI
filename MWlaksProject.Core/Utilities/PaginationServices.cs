using Microsoft.EntityFrameworkCore;
using MWlaksProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Utilities
{
    public  class PaginationServices
    {
        public  async Task<(IQueryable<T> Data ,PaginationMetaData pagination)> GetPaginatedResultAsync<T>
            (IQueryable<T> queryable,
            int page,
            int limit) where T : class
        {
            var TotalCount = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling(TotalCount /(double) limit);
            var currentPage = page;
            var nextPage = currentPage > limit ? currentPage+1 : (int?) null;
            var prevpage = currentPage > 1 ? currentPage - 1 : (int?)null;

            var paginationData = queryable
                .Skip((currentPage - 1) * limit)
                .Take(limit);

            var pagination = new PaginationMetaData
            {
                TotalPages = totalPages,
                currentpage = currentPage,
                Nextpaget = nextPage,
                PrevPage = prevpage,
                TotalRecords = TotalCount,
            };

            return (data: paginationData, pagination: pagination);

        }
    }
}
