﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.SpecialityModels;
using Application.Models.UniversityAdmin;

namespace Application.IServices.UniversityAdmin
{
    public interface ISpecialityService
    {
        Task<bool> AddSpecialityAsync(AddSpecialityModel model);
        Task DeleteSpecialityAsync(int specialityId);
        Task<bool> DeleteSpecialityAndSaveAsync(int specialityId);
        Task<List<SpecialityInfoModel>> SearchSpecialityAsync(string filter);
        Task<SpecialityRating> GetSpecialityRatingAsync(int specialityId);

    }
}