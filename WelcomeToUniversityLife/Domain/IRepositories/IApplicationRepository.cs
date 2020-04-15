using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IApplicationRepository : IRepository<Request, int>
    {
        Task<List<Request>> GetAllRequestsByUserId(int userId);

        Task<List<Request>> GetAllRequestsBySpecialityId(int specialityId);
    }
}