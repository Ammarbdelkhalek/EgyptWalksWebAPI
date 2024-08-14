using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWalksProject.Infastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        } 
        public DbSet<Walks> Walks { get; set; }
        public DbSet<FaviouriteWalks> FaviouriteWalks { get; set; }
        public DbSet<Difficulty>difficulties { get; set; }
        public DbSet<Images>Images { get; set; }
        public DbSet<Region>Regions { get; set; }
        public DbSet<Review> Reviews { get; set; }


    }
}
