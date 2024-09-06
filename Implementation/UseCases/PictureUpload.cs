using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Implementation.UseCases
{
    public static class PictureUpload
    {
        public static string Upload(IFormFile image,string folderPath)
        {
            List<string> allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
            if (image == null)
            {
                throw new ConflictException("You have to upload picture for product");
            }

            var extension = Path.GetExtension(image.FileName);
            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                throw new ConflictException("Invalid file extension.");
            }

            var fileName = Guid.NewGuid().ToString() + extension;
            
                var savePath = Path.Combine("wwwroot", folderPath, fileName);
                using var fs = new FileStream(savePath, FileMode.Create);
                image.CopyTo(fs);

                return $"\\{folderPath}\\{fileName}";
            
            
        }
    }
}
