using Application.Models.UniversityAdmin;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUniversityService
    {
        Task<bool> EditUniversity(University model);
        Task<CurrentUniversityAndFacultiesModel> GetUniversity();
        Task<CurrentUniversityAndFacultiesModel> GetUniversityAsync(int universityId);
        Task UploadUniversityPhotoAsync(UploadPhotoModel requestedData, IFormFileCollection uploads);
        Task DeleteUniversityPhotoAsync(int userId);
    }
}