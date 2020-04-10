using System.Threading.Tasks;
using Application.Models.User;

namespace Application.IServices
{
    public interface IZnoService
    {
        Task<bool> SaveZNOMarks(AddMarksModel model);
    }
}