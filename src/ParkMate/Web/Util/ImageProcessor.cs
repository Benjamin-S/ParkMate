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

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "ImageUploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Resize image once streamed to server and overwrite previous file
            // TODO: Find a way to do this at the time of streaming to server to limit IO operations
            ResizeImage(filePath);

            return new ImageValidationResult
            {
                IsValid = true,
                FileName = fileName
            };
        }

        public ImageValidationResult IsValidImage(IFormFile image)
        {
            // TODO: Validate based on minimum size restrictions when agreed upon
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

        private void ResizeImage(string fileName)
        {
            // TODO: Implement scaled resizing based on image size restrictions
            using (Image<Rgba32> image = Image.Load(fileName))
            {
                // HACK: Temporary resizing of 50% for functionality
                image.Mutate(x => x
                    .Resize(image.Width / 2, image.Height / 2)
                );
                image.Save(fileName);
            }
        }
    }
}