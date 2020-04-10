using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    public interface IPhotoHelper
    {
        Task<string> UploadPhotoAsync(IFormFile file, string folder);

        void DeletePhotoAsync(string name, string folder);
    }
}