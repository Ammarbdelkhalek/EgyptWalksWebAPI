using Microsoft.AspNetCore.Http;
using MWlaksProject.Core.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.WalksDTOS
{
    public class UpdateWalksDto
    {
        [Required(), MaxLength(150, ErrorMessage = "Name Should be at least 150")]
        public string Name { get; set; }
        [Required(), MaxLength(150, ErrorMessage = "Description Should be at least 150")]

        public string Description { get; set; }
        [Required(), Range(1, 10)]

        public double ?LengthInKm { get; set; }
        [FileValidator("jpg ,png", 1)]
        public IFormFile? WalkImageUrl { get; set; }
        [Required(ErrorMessage = "DifficultyId field is required")]
        public Guid ?DifficultyId { get; set; }
        [Required(ErrorMessage = "RegionId field is required")]
        public Guid ?RegionId { get; set; }
    }
}
