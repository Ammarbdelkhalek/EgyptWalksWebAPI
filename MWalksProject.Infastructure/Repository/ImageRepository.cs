using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MWalksProject.Infastructure.Data;
using MWlaksProject.Core.DTOS.ImagesDtos;
using MWlaksProject.Core.Interfaces;
using MWlaksProject.Core.Models;

namespace MWalksProject.Infastructure.Repository
{
    public class ImageRepository(IHostingEnvironment environment,
     IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) : IimageRepository
    {
        public async Task<Images> UploadImageAsync(ImageDto dto)
        {
            var UniqueName = Guid.NewGuid();
            var Extention = Path.GetExtension(dto.image.FileName);
            var fileName = $"{UniqueName}{Extention}";
            var UploadDirectory = Path.Combine(environment.ContentRootPath, "Images");
            var SavePath = Path.Combine(UploadDirectory, fileName);
            if(!Directory.Exists(UploadDirectory))
            {
                Directory.CreateDirectory(UploadDirectory);
            }
            
             using var stream = new FileStream(SavePath, FileMode.Create);
            await dto.image.CopyToAsync(stream);
            var request = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{fileName}";
            var image = new Images
            {
                Id = UniqueName,
                FileName = fileName,
                FilePath = urlPath,
                FileExtension = Extention,
                FileSizeInBytes = dto.image.Length,
                FileDescription = dto.FileDescription
            };
            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteImage(Guid id)
        {
            var image = context.Images.FirstOrDefault(x=>x.Id==id); 
            if (image == null)
            {
                return false;
            }
            var UploadFile = Path.Combine(environment.WebRootPath, "Images");
            var filePath = Path.Combine(UploadFile,image.FileName);
            if (!File.Exists(filePath))
            {
                return false;
            }
            File.Delete(filePath);
             context.Images.Remove(image);
            await context.SaveChangesAsync();

            return  true;
        }
    }
}
