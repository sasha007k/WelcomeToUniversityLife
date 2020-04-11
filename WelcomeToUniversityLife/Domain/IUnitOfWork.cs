using System.Threading.Tasks;
using Domain.IRepositories;

namespace Domain
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IZNORepository ZNORepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IDocumentRepository DocumentRepository { get; }     
        ICampaignRepository CampaignRepository { get; }

        Task<int> Commit();
    }
}