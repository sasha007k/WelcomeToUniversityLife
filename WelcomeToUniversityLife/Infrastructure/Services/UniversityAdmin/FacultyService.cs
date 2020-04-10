using System.Threading.Tasks;
using Application.IServices.UniversityAdmin;
using Application.Models.UniversityAdmin;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.UniversityAdmin
{
    public class FacultyService : IFacultyService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public FacultyService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddFacultyAsync(AddFacultyModel model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = _unitOfWork.UniversityRepository.GetUniversityWithUserId(user.Id).Result;

                var faculty = new Faculty
                {
                    Name = model.FacultyName,
                    Address = model.Address,
                    Description = model.Description,
                    UniversityId = university.Id
                };

                await _unitOfWork.FacultyRepository.CreateAsync(faculty);
                var result = await _unitOfWork.Commit();

                return result == 1;
            }

            return false;
        }

        public async Task<bool> EditFaculty(Faculty model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var faculty = _unitOfWork.FacultyRepository.GetAsync(model.Id).Result;

                var result = 0;

                if (faculty != null)
                {
                    faculty.Name = model.Name;
                    faculty.Address = model.Address;
                    faculty.Description = model.Description;

                    result = await _unitOfWork.Commit();
                }

                return result == 1;
            }

            return false;
        }

        public async Task<CurrentFacultyAndSpecialitiesModel> GetFacultyAsync(int facultyId)
        {
            var faculty = await _unitOfWork.FacultyRepository.GetAsync(facultyId);

            var currentFacultyAndSpecialities = new CurrentFacultyAndSpecialitiesModel();

            if (faculty != null)
            {
                currentFacultyAndSpecialities.CurrentFaculty = faculty;

                var specialities = _unitOfWork.SpecialityRepository.GetAllSpecialitiesWithFacultyId(facultyId).Result;

                currentFacultyAndSpecialities.Specialities = specialities;

                var university = await _unitOfWork.UniversityRepository.GetUniversityWithId(faculty.UniversityId);

                currentFacultyAndSpecialities.FacultyAdminId = university.UserId;

                return currentFacultyAndSpecialities;
            }

            return null;
        }
    }
}