using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.RegionDTOS
{
    public class AddRegionDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

