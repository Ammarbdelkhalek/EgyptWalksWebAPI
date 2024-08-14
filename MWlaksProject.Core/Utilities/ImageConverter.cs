using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Utilities
{
    public static class ImageConverter
    {
        public  static async Task<byte[]>ToByteArray (this IFormFile image)
        {
            using (var stream = new MemoryStream ())
            {
                await image.CopyToAsync (stream);
                return stream.ToArray ();
            }
        }
    }
}
