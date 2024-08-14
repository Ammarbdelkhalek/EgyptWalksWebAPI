using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.DTOS.WalksDTOS;
using MWlaksProject.Core.exception;
using MWlaksProject.Core.Helper;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;
using MWlaksProject.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MWalksProject.Infastructure.Repository
{
    public class WalksRepository(ApplicationDbContext context , PaginationServices paginationService, IMapper mapper) : IWalkRepository
    {

        public async Task<(IEnumerable<Walks> Data, PaginationMetaData Pagination)> GetAllWalksAsync
            ( 
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool ascending = true,
            int page = 1,
            int limit = 5)
        {
            var walks = context.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                walks = filterOn.ToLower() switch
                {
                    "name" => walks.Where(x => x.Name.Contains(filterQuery)),
                    "lengthinkm" when double.TryParse(filterQuery, out double lengthInKm) => walks.Where(x => x.LengthInKm == lengthInKm),
                    _ => walks
                };
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                walks = sortBy.ToLower() switch
                {
                    "name" => ascending ?
                        walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name),
                    "lengthinkm" => ascending ?
                        walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm),
                    "region" => ascending ?
                        walks.OrderBy(x => x.Region.Name) : walks.OrderByDescending(x => x.Region.Name),
                    "difficulty" => ascending ?
                        walks.OrderBy(x => x.Difficulty.Name) : walks.OrderByDescending(x => x.Difficulty.Name),
                    _ => walks
                };
            }

            // Pagination
            var (paginatedWalks, pagination) = await paginationService.GetPaginatedResultAsync(walks, page, limit);

            return (Data: await paginatedWalks.ToListAsync(), Pagination: pagination);
        }


        public async Task<IEnumerable<Walks>> GetAllAsync(QuaryObject quaryObject)
        {
            var walks = context.Walks.Include(x=>x.Difficulty).Include(x=>x.Region).AsNoTracking().AsQueryable();
            
            //filter

            if(string.IsNullOrWhiteSpace(quaryObject.filterOn) ==false&&string.IsNullOrWhiteSpace(quaryObject.filterQuery) == false)
            {
                if (quaryObject.filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks.Where(x => x.Name.Contains(quaryObject.filterQuery));
                }
            }

            // sorting
            if(string.IsNullOrWhiteSpace(quaryObject.sortBy) == false)
            {
                if (quaryObject.sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)quaryObject.isAscending ? walks.OrderBy(x=>x.Name) :walks.OrderByDescending(x=>x.Name);
                }
                else if (quaryObject.sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)quaryObject.isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }

            }
            //pagination 
            var SkipResult = (quaryObject.pageNumber - 1) * quaryObject.pageSize;
            return await walks.Skip(SkipResult).Take(quaryObject.pageSize).ToListAsync();
        }

        public async Task<Walks> GetByIdAsync(Guid id)
        {
            var walk =  await context.Walks.FirstOrDefaultAsync(x=>x.Id == id);
            if(walk == null)
            {
                throw new NullReferenceException("Walks not found");
            }
            return walk;
        }

        public async Task<Walks> CreateAsync(AddWalkDto dto)
        {

            var region = await context.Regions.FindAsync(dto.RegionId);
            if (region == null)
                throw new NotFoundException($"Region with id {dto.RegionId} not found.");


            var difficulty = await context.difficulties.FindAsync(dto.DifficultyId);
            if (difficulty == null)
                throw new NotFoundException($"Difficulty with id {dto.DifficultyId} not found.");

            var walk = mapper.Map<Walks>(dto);
            walk.WalkImageUrl = dto.WalkImageUrl != null ? await MWlaksProject.Core.Utilities.ImageConverter.ToByteArray(dto.WalkImageUrl) : null;
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walks> updateAsync(UpdateWalksDto dto,Guid id)
        {
            var existingWalk = await context.Walks.FindAsync(id);
            if (existingWalk == null)
                throw new NotFoundException($"Walk with id ${id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                existingWalk.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                existingWalk.Description = dto.Description;

            if (dto.LengthInKm.HasValue)
                existingWalk.LengthInKm = dto.LengthInKm.Value;

            if (dto.WalkImageUrl != null)
                existingWalk.WalkImageUrl = await MWlaksProject.Core.Utilities.ImageConverter.ToByteArray(dto.WalkImageUrl);

            if (dto.RegionId.HasValue)
            {
                var existingRegion = await context.Regions.FindAsync(dto.RegionId);
                if (existingRegion == null)
                    throw new NotFoundException($"Region with id {dto.RegionId} not found.");

                existingWalk.RegionId = dto.RegionId.Value;
            }

            if (dto.DifficultyId.HasValue)
            {
                var existingDifficulty = await context.difficulties.FindAsync(dto.DifficultyId);
                if (existingDifficulty == null)
                    throw new NotFoundException($"Difficulty with id {dto.DifficultyId} not found.");

                existingWalk.DifficultyId = dto.DifficultyId.Value;
            }

            await context.SaveChangesAsync();
            return existingWalk;

        }
        public async Task<Walks> deleteAsync(Guid id)
        {
            var ExistWalk = await GetByIdAsync(id);
            if(ExistWalk != null)
            {
                context.Remove(ExistWalk);
                await context.SaveChangesAsync();
                return ExistWalk;
            }
            else
            {
                return null;
            }
        }

        
    }
}
