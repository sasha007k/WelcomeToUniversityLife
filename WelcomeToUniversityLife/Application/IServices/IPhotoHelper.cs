using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IPhotoHelper
    {
        Task<string> UploadPhotoAsync(IFormFile file, string folder);

        void DeletePhotoAsync(string name, string folder);
    }
}