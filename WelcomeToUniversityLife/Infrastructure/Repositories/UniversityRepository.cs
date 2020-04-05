using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UniversityRepository:Repository<University,int>,IUniversityRepository
    {
        public UniversityRepository(DatabaseContext context):base(context)
        {
        }

        public Task<University> GetUniversityWityUser(int universityId)
        {
            return this._context.Universities
                .Where(uni => uni.Id == universityId)
                .Include(uni => uni.User)
                .FirstOrDefaultAsync();             
        }
    }
}
