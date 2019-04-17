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

        public ImageValidationResult SaveImage(IFormFile img)
        {
            try
            {
                return ProcessImage(img);
            }
            catch(NotSupportedException)
            {
                return new ImageValidationResult
                {
                    FileName = "Not a valid image file. Please upload a JPG, PNG, BMP or GIF"
                };
            }
        }
        ImageValidationResult ProcessImage(IFormFile img)
        {
            if (img.Length > 10 * 1024 * 1024)
            {
                return new ImageValidationResult
                {
                    FileName = "Upload can not be larger than 10MB. Please upload a smaller image."
                };
            }
            using (var stream = img.OpenReadStream())
            using (Image<Rgba32> image = Image.Load(stream))
            {

                if (image.Height < 480 || image.Width < 480)
                {
                    return new ImageValidationResult
                    {
                        FileName = "Image is too small. Please upload a large image."
                    };
                }

                if (image.Width > 1280)
                {
                    image.Mutate(x => x.Resize(1280, 0));
                }

                if (image.Height > 1280)
                {
                    image.Mutate(x => x.Resize(0, 1280));
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "ImageUploads", fileName);

                using (var file = new FileStream(filePath, FileMode.Create))
                {
                    image.SaveAsJpeg(file);

                    return new ImageValidationResult
                    {
                        IsValid = true,
                        FileName = fileName
                    };
                }
            }
        }
    }
}