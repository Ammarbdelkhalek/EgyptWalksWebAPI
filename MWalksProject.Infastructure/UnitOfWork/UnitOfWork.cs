using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MWalksProject.Infastructure.Data;
using MWalksProject.Infastructure.Repository;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.IUnitOfWork;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWalksProject.Infastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment environment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IWalkRepository Walks { get;private set; }

        public IGenericInterfaceRepo<MWlaksProject.Core.Models.Region> Region { get; private set; }

        public IGenericInterfaceRepo<Difficulty> Difficulty { get; private set; }

        public IReviewRepsitory Reviews { get; private set; }

        public IFaviouriteWalks FavoriteWalks { get; private set; }

        public IimageRepository Images { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor,PaginationServices pagination,IMapper mapper )
        {
           _context = context;
            Walks = new WalksRepository(context,pagination,mapper);
            Region =  new RegionRepository(context);
            Difficulty =  new DifficultyRepository(context);
            Reviews = new ReviewRepository(context,mapper);
            FavoriteWalks =new FaviouriteWalksRepositoy(context) ;
            Images = new ImageRepository(environment,httpContextAccessor,context);
        }

        

        public int Complete()
        {
            return  _context.SaveChanges();
           
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
