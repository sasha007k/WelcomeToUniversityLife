using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.SiteAdmin;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.IServices
{
    public interface ISiteAdminService
    {
        Task<bool> AddUniversityAsync(AddUniversityModel model);
        List<University> GetAllUniversities();
    }
}
