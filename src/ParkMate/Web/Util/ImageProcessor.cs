using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Algorithm;
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
            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "ImageUploads", fileName);

            Image<Rgba32> finalImage = null;

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                using (var readStream = image.OpenReadStream())
                {
                    finalImage = Image.Load(fileBytes);
                }

                var validation = IsValidImage(image);
                var smallImage = IsImageTooSmall(finalImage);

                if (!validation.IsValid || smallImage)
                {
                    return new ImageValidationResult
                    {
                        IsValid = false
                    };
                }
                finalImage = ResizeImage(finalImage);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                finalImage.SaveAsJpeg(stream);
            }

            return new ImageValidationResult
            {
                IsValid = true,
                FileName = fileName
            };
        }

        private ImageValidationResult IsValidImage(IFormFile image)
        {
            // Maximum image size is 10MB, reject anything over that
            const long maxFileSize = 10 * 1024 * 1024;

            var isImage = image.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0;
            var isValidSize = image.Length < maxFileSize;

            if (!isImage || !isValidSize)
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

        private Image<Rgba32> ResizeImage(Image<Rgba32> image)
        {
            const int width = 1280;
            const int height = 720;
            var isLandscape = image.Width >= image.Height;
            var isTooLarge = isLandscape ? image.Width > width : image.Height > height;

            if (!isTooLarge)
            {
                return image;
            }
            if (isLandscape)
            {
                image.Mutate(x => x
                    .Resize(width, 0)
                );
            }
            else
            {
                image.Mutate(x => x
                    .Resize(0, height)
                );
            }
            return image;
        }

        private bool IsImageTooSmall(Image<Rgba32> image)
        {
            const int minWidth = 640;
            const int minHeight = 480;
            var isLandscape = image.Width >= image.Height;

            if (isLandscape)
            {
                return image.Width < minWidth || image.Height < minHeight;
            }
            else
            {
                return image.Width < minHeight || image.Height < minWidth;
            }
        }
    }
}