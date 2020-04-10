using System.Threading.Tasks;
using Domain;
using Domain.IRepositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationRepository applicationRepository;
        private IDocumentRepository documentRepository;
        private IFacultyRepository facultyRepository;
        private ISpecialityRepository specialityRepository;
        private IUniversityRepository universityRepository;
        private IUserRepository userRepository;
        private IZNORepository znoRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public DatabaseContext _context { get; }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository(_context);

                return userRepository;
            }
        }

        public IZNORepository ZNORepository
        {
            get
            {
                if (znoRepository == null) znoRepository = new ZnoRepository(_context);

                return znoRepository;
            }
        }

        public IApplicationRepository ApplicationRepository
        {
            get
            {
                if (applicationRepository == null) applicationRepository = new ApplicationRepository(_context);

                return applicationRepository;
            }
        }

        public IFacultyRepository FacultyRepository
        {
            get
            {
                if (facultyRepository == null) facultyRepository = new FacultyRepository(_context);

                return facultyRepository;
            }
        }

        public ISpecialityRepository SpecialityRepository
        {
            get
            {
                if (specialityRepository == null) specialityRepository = new SpecialityRepository(_context);

                return specialityRepository;
            }
        }

        public IUniversityRepository UniversityRepository
        {
            get
            {
                if (universityRepository == null) universityRepository = new UniversityRepository(_context);

                return universityRepository;
            }
        }

        public IDocumentRepository DocumentRepository
        {
            get
            {
                if (documentRepository == null) documentRepository = new DocumentRepository(_context);

                return documentRepository;
            }
        }

        public Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }
    }
}