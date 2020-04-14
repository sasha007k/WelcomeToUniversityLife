using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IServices.UniversityAdmin;
using Application.Models.Enum;
using Application.Models.SpecialityModels;
using Application.Models.UniversityAdmin;
using Domain;
using Domain.Entities;

namespace Infrastructure.Services.UniversityAdmin
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddSpecialityAsync(AddSpecialityModel model)
        {
            var speciality = new Speciality
            {
                Description = model.Description,
                Name = model.SpecialityName,
                FacultyId = model.FacultyId,
                FreeSpaces = model.FreeSpaces,
                PaidSpaces = model.PaidSpaces,
                RequiredZNO1 = AllZNO.GetZNOName(ZNOs.Ukrainian)
            };

            if (model.ZNO != null)
            {
                switch (model.ZNO.Count)
                {
                    case 1:
                        speciality.RequiredZNO2 = model.ZNO[0];
                        break;
                    case 2:
                        speciality.RequiredZNO2 = model.ZNO[0];
                        speciality.RequiredZNO3 = model.ZNO[1];
                        break;
                    //case 3:
                    //    speciality.RequiredZNO2 = model.ZNO[0];
                    //    speciality.RequiredZNO3 = model.ZNO[1];
                    //    speciality.RequiredZNO4 = model.ZNO[2];
                    //    break;
                    default:
                        break;
                }
            }

            await _unitOfWork.SpecialityRepository.CreateAsync(speciality);
            var result = await _unitOfWork.Commit();

            return result == 1;
        }

        public async Task DeleteSpecialityAsync(int specialityId)
        {
            await _unitOfWork.SpecialityRepository.DeleteAsync(specialityId);
        }

        public async Task<bool> DeleteSpecialityAndSaveAsync(int specialityId)
        {
            await _unitOfWork.SpecialityRepository.DeleteAsync(specialityId);
            return await _unitOfWork.Commit() == 1;
        }

        public async Task<List<SpecialityInfoModel>> SearchSpecialityAsync(string filter)
        {
            var specialities = await _unitOfWork.SpecialityRepository.SearchSpeciality(filter);

            var specialitiesResponce = new List<SpecialityInfoModel>();

            foreach(var spec in specialities)
            {
                specialitiesResponce.Add(new SpecialityInfoModel
                {
                    id = spec.Id,
                    SpecialityName = spec.Name,
                    FacultyName = spec.Faculty.Name,
                    UniversityName = spec.Faculty.University.Name
                });
            }

            return specialitiesResponce;     
        }
    }
}