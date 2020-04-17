using Application.Models.UniversityAdmin;
using Domain.Entities;
using System.Threading.Tasks;

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