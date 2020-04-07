using System.Collections.Generic;
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

        public Task<University> GetUniversityWithUser(int universityId)
        {
            return this._context.Universities
                .Where(uni => uni.Id == universityId)
                .Include(uni => uni.User)
                .FirstOrDefaultAsync();             
        }

        public Task<List<University>> GetAllUniversitities()
        {
            return (from university in _context.Universities
                                    join user in _context.Users on university.UserId equals user.Id
                select new University()
                {
                    Id = university.Id,
                    Name = university.Name,
                    Photo = university.Photo,
                    City = university.City,
                    Description = university.Description,
                    User = user

                }).ToListAsync();
        }

        public Task<University> GetUniversityWithUserId(int userId)
        {
            return this._context.Universities
                .SingleAsync(u => u.UserId == userId);
        }

        public Task<University> GetUniversityWithId(int universityId)
        {
            return _context.Universities.FirstOrDefaultAsync(uni => uni.Id == universityId);
        }
    }
}
