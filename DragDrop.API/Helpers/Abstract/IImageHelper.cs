using DragDrop.API.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DragDrop.API.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IResult> UploadedImage(IFormFile imageFile);
        Task<IResult> Delete(string pictureName);
    }
}
