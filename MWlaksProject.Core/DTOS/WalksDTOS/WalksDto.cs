using Microsoft.AspNetCore.Http;
using MWlaksProject.Core.CustomValidationAttribute;
using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.WalksDTO
{
    public class WalksDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        [FileValidator("jpg ,png",1)]
        public IFormFile? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
