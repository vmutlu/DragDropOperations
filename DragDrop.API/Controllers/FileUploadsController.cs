using DragDrop.API.Helpers.Abstract;
using DragDrop.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DragDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        public IWebHostEnvironment _env { get; set; }
        public IImageHelper _imageHelper { get; set; }
        public FileUploadsController(IWebHostEnvironment env, IImageHelper imageHelper)
        {
            _env = env;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile formFile)
        {
            if (formFile != null)
            {
                var uploadedImages = await _imageHelper.UploadedImage(formFile);
                return Ok(uploadedImages);
            }

            else
                return BadRequest("Dosya yüklemek için lütfen geçerli bir dosya yüklediğinizde emin olunuz.");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteImage(ImageModel imageModel)
        {
            if (imageModel.ImagePath != null)
            {
                var uploadedImages = await _imageHelper.Delete(imageModel.ImagePath);
                return Ok(uploadedImages);
            }

            else
                return BadRequest("Dosya yüklemek için lütfen geçerli bir dosya yüklediğinizde emin olunuz.");
        }
    }
}
