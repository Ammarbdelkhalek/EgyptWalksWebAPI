
using Microsoft.EntityFrameworkCore;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.DTOS.RegionDTOS;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;


namespace MWalksProject.Infastructure.Repository
{
    public class RegionRepository(ApplicationDbContext contex) : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return await contex.Regions.AsNoTracking().ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            var result =  await contex.Regions.FindAsync(id); 
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Region> updateAsync(Region entity , Guid id)
        {
            var ExistRegion = await GetByIdAsync(id);
            if (ExistRegion!=null)
            {
                ExistRegion.Name = entity.Name;
                ExistRegion.Code = entity.Code;
                ExistRegion.RegionImageUrl = entity.RegionImageUrl;
                await contex.SaveChangesAsync();
                return entity;
            }
            return null;
        }
        public async  Task<Region> CreateAsync(Region entity)
        {

            await contex.Regions.AddAsync(entity);
            await contex.SaveChangesAsync();    
            return entity;
        }

        public async Task<Region> deleteAsync(Guid id)
        {
           var region =await  GetByIdAsync(id);
            if (region != null)
            {
                 contex.Regions.Remove(region);
                await contex.SaveChangesAsync();
                return region;
            }
            else
            {
                return null;
            }

        }

       
    }
}
