using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.UniversityAdmin;
using Microsoft.AspNetCore.Mvc;

namespace Application.IServices
{
    public interface IUniversityAdminService
    {
        Task<CurrentUniversityAndFaculties> GetUniversity();
        Task<CurrentUniversityAndFaculties> GetUniversityAsync(int universityId);
        Task<bool> EditUniversity(UniversityInfo model);
    }
}
