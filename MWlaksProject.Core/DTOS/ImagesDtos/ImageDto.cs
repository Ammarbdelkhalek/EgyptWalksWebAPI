using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MWlaksProject.Core.DTOS.ImagesDtos
{
    public class ImageDto
    {
        public IFormFile image {  get; set; }
        public string FileName { get; set; }
        public string? FileDescription {  get; set; }

    }
}
