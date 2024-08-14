using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Interfaces
{
    public interface IFaviouriteWalks
    {
        Task<List<FaviouriteWalks>> GetAllFaviouriteWalks();
        Task<FaviouriteWalks> Add(FaviouriteWalks faviouriteWalks);
        Task Remove(Guid id);
    }
}
