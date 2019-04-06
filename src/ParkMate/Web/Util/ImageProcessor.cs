using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ParkMate.Web.Util
{
    public class ImageProcessor
    {
        private readonly IHostingEnvironment _environment;

        public ImageProcessor(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ImageValidationResult> SaveImage(IFormFile image)
        {
            var validation = IsValidImage(image);
            if (!validation.IsValid)
            {
                return validation;
            }

            // Resize image, will probably need to convert to something other than IFormFile

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "ImageUploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                ResizeImage(stream, filePath);
            }

            return new ImageValidationResult
            {
                IsValid = true,
                FileName = fileName
            };
        }

        public ImageValidationResult IsValidImage(IFormFile image)
        {
            // validate that image file is valid, and is at least of minimum acceptable size
            if (image.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return new ImageValidationResult
                {
                    IsValid = false
                };
            }
            return new ImageValidationResult
            {
                IsValid = true
            };
        }

        void ResizeImage(Stream stream, string filename)
        {
            // resize image so we're not saving images that are larger than necessary 
            using (Image<Rgba32> image = Image.Load(stream))
            {
                image.Mutate(x => x
                    .Resize(image.Width / 2, image.Height / 2)
                );
                image.Save(filename);
            }
        }
    }
}