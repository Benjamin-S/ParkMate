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
                using (var imageOutput = Image.Load(stream))
                {
                    ResizeImage(imageOutput).SaveAsJpeg(stream);
                }
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

            var isImage = image.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) <0;
            var isValidSize = image.Length < maxFileSize;

            if (!isImage || !isValidSize)
            {
                return new ImageValidationResult
                {
                    IsValid = false
                };
            }
            
            // Load IFormFile to memory stream and create an image object to check for min size
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                using (Image<Rgba32> tempImage = Image.Load<Rgba32>(memoryStream.ToArray()))
                {
                    return new ImageValidationResult
                    {
                        IsValid = !IsImageTooSmall((tempImage))
                    };
                }
            }
        }

        private Image<Rgba32> ResizeImage(Image<Rgba32> image)
        {
                const int width = 1280;
                const int height = 720;
                var isLandscape = image.Width >= image.Height;
                var isTooLarge = isLandscape ? image.Width > 1280 : image.Height > 1600;

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