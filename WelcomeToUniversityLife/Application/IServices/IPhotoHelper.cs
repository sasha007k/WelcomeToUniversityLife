using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IPhotoHelperService
    {
        Task<string> UploadPhotoAsync(IFormFile file, string folder);

        void DeletePhotoAsync(string name, string folder);
    }
}