using System.Threading.Tasks;
using Application.Models.UniversityAdmin;
using Domain.Entities;

namespace Application.IServices.UniversityAdmin
{
    public interface IFacultyService
    {
        Task<bool> AddFacultyAsync(AddFacultyModel model);
        Task<bool> EditFaculty(Faculty model);
        Task<CurrentFacultyAndSpecialitiesModel> GetFacultyAsync(int facultyId);
        Task<bool> DeleteFaculty(int facultyId);
    }
}