using Domain.IRepositories;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get;}
        IZNORepository ZNORepository { get;}
        IApplicationRepository ApplicationRepository { get;}
        IFacultyRepository FacultyRepository { get;}
        ISpecialityRepository SpecialityRepository { get;}
        IUniversityRepository UniversityRepository { get;}
        IDocumentRepository DocumentRepository { get;}

        Task Commit();
    }
}
