using EPHARMA.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Services
{
    public class ImageService : IImageInterface
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string AddImage(IFormFile ImageName)
        {
            string uniqueFileName = "";
            if (ImageName != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageName.CopyTo(fileStream);
                    fileStream.Close();
                }

                return uniqueFileName;
            }
            return uniqueFileName;
        }

        public bool RemoveImage(string ImageName)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "PharmacyImages\\" + ImageName);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;

            }
            return false;
        }
    }
}
