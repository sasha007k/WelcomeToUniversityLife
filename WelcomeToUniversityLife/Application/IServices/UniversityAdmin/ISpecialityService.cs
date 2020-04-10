using System.Threading.Tasks;
using Application.Models.UniversityAdmin;

namespace Application.IServices.UniversityAdmin
{
    public interface ISpecialityService
    {
        Task<bool> AddSpecialityAsync(AddSpecialityModel model);
    }
}