using Application.Models.User;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IZnoService
    {
        Task<bool> SaveZNOMarks(AddMarksModel model);
    }
}