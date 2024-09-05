using Microsoft.EntityFrameworkCore;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;

namespace MWalksProject.Infastructure.Repository
{
    public class DifficultyRepository (ApplicationDbContext context): IDifficultiesRepository
    {

        public async  Task<List<Difficulty>> GetAllAsync()
        {
            return await context.difficulties.ToListAsync(); 
        }

        public async Task<Difficulty> CreateAsync(Difficulty entity)
        {
            await context.difficulties.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
           
        }

        public async Task<Difficulty> deleteAsync(Guid id)
        {
            var ExistDifficult = await context.difficulties.FindAsync(id);
            if(ExistDifficult != null)
            {
                 context.difficulties.Remove(ExistDifficult);
                await context.SaveChangesAsync();
                return ExistDifficult;
            }
            else
            {
                return null;
            }
        }
        public async Task<Difficulty> GetByIdAsync(Guid id)
        {
            var Diffcult = await context.difficulties.FindAsync(id);
            if(Diffcult != null)
            {
                return Diffcult;
            }
            return null;
        }

        public async Task<Difficulty> updateAsync(Difficulty entity, Guid id)
        {
            var existDifficult = await context.difficulties.FindAsync(id);
            if(existDifficult!= null)
            {
                existDifficult.Name = entity.Name;
                await context.SaveChangesAsync();
                return existDifficult;

            }
            return null;
        }
    }
}
