using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.SiteAdmin;
using Domain.Entities;

namespace Application.IServices
{
    public interface ISiteAdminService
    {
        Task<bool> AddUniversityAsync(AddUniversityModel model);
        List<University> GetAllUniversities();
    }
}