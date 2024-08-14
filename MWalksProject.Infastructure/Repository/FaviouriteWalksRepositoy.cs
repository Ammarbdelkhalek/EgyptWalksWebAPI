using Microsoft.EntityFrameworkCore;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;


namespace MWalksProject.Infastructure.Repository
{
    public class FaviouriteWalksRepositoy(ApplicationDbContext context) : IFaviouriteWalks
    {

        public async  Task<List<FaviouriteWalks>> GetAllFaviouriteWalks()
        {
            return   await context.FaviouriteWalks.AsNoTracking().ToListAsync();
        }
        public async Task<MWlaksProject.Core.Models.FaviouriteWalks> Add(MWlaksProject.Core.Models.FaviouriteWalks faviouriteWalks)
        {
            await context.FaviouriteWalks.AddAsync(faviouriteWalks);
            await context.SaveChangesAsync();
            return faviouriteWalks;
        }

        public async Task Remove(Guid id)
        {
            var ExistFaviourtieWalk = await context.FaviouriteWalks.FindAsync(id);
            if(ExistFaviourtieWalk != null) {
                context.FaviouriteWalks.Remove(ExistFaviourtieWalk);
                await context.SaveChangesAsync();
            }
        }
    }
}
