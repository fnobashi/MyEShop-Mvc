using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MyEShop.Constants;
using System.Text.RegularExpressions;

namespace MyEShop.Utilities
{
    public static class ImageUploader
    {
        public static async Task<UploadResult> UploadAsync(IFormFile file, string path)
        {
            var type = file.ContentType;
            if (!type.ToLower().Contains("jpeg") && !type.ToLower().Contains("png"))
                return new UploadResult
                {
                    IsSuccess = false,
                    IsValidExtension = false
                };

            

            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var savepath = Path.Combine(path, filename);
                using (var stream = new FileStream(savepath , FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new UploadResult
                {
                    FileName = filename,
                    IsValidExtension = true ,
                    IsSuccess = true
                };
            }
            catch
            {
                return new UploadResult
                {
                    IsValidExtension = true,
                    IsSuccess = false,
                };
            }
        }
    }
    public class UploadResult
    {
        public bool IsSuccess { get; set; }
        public bool IsValidExtension { get; set; }
        public string? FileName { get; set; }

    }
}