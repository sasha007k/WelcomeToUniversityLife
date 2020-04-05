using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.UniversityAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.IServices
{
    public interface IUniversityAdminService
    {
        Task<CurrentUniversityAndFacultiesModel> GetUniversity();
        Task<CurrentUniversityAndFacultiesModel> GetUniversityAsync(int universityId);
        Task<bool> EditUniversity(UniversityInfoModel model);
        Task<bool> AddFacultyAsync(AddFacultyModel model);
        Task<CurrentFacultyAndSpecialitiesModel> GetFacultyAsync(int facultyId);
        Task<bool> AddSpecialityAsync(AddSpecialityModel model);
        Task UploadUniversityPhotoAsync(UploadPhotoModel requestedData, IFormFileCollection uploads);
    }
}
