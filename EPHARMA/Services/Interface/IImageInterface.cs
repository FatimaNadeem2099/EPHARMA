using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Services.Interface
{
    public interface IImageInterface
    {
        public string AddImage(IFormFile ImageName);
        public bool RemoveImage(string ImageName);
    }
}
