using System.Threading.Tasks;
using Application.IServices.UniversityAdmin;
using Application.Models.Enum;
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

            switch (model.ZNO.Count)
            {
                case 1:
                    speciality.RequiredZNO2 = model.ZNO[0];
                    break;
                case 2:
                    speciality.RequiredZNO2 = model.ZNO[0];
                    speciality.RequiredZNO3 = model.ZNO[1];
                    break;
            }

            await _unitOfWork.SpecialityRepository.CreateAsync(speciality);
            var result = await _unitOfWork.Commit();

            return result == 1;
        }
    }
}