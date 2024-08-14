using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IWalkRepository Walks { get; }
        IGenericInterfaceRepo<Region> Region { get; }
        IGenericInterfaceRepo<Difficulty> Difficulty { get; }
        IReviewRepsitory Reviews { get; }
        IFaviouriteWalks FavoriteWalks { get; }
        IimageRepository Images { get; }

        int Complete();

    }
}
