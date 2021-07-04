using DragDrop.API.Helpers.Abstract;
using DragDrop.API.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragDrop.API.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string _imgFolder = "UploadedImages";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageHelper(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IResult> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}\\{_imgFolder}\\", pictureName);
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
                return new Result(success: true, message: pictureName + " dizininde yer alan resim başarıyla silindi."); ;
            }

            else
                return new ErrorResult(message: pictureName + " dizininde belirtilen isimde dosya bulunamadı.");
        }

        public async Task<IResult> UploadedImage(IFormFile imageFile)
        {
            if (!Directory.Exists($"{_wwwroot}\\{_imgFolder}"))
                Directory.CreateDirectory($"{_wwwroot}\\{_imgFolder}");

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension = Path.GetExtension(imageFile.FileName);

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileName.ToLower() == ".tiff")
            {
                DateTime dateTime = DateTime.Now;
                string fullFileName = $"{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName}{fileExtension}";

                string path = Path.Combine($"{_wwwroot}\\{_imgFolder}\\" + fullFileName);
                string clientAccessPath = Path.Combine(@"\UploadedImages\" + fullFileName);

                await using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return new Result(success: true, message: clientAccessPath);
            }

            return new ErrorResult(message: "Yüklemeye çalıştığınız dosya resim formatında olmak zorundadır.");
        }
    }
}
