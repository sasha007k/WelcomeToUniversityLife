using Domain;
using Domain.IRepositories;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository userRepository;
        private IZNORepository znoRepository;
        private IApplicationRepository applicationRepository;
        private IUniversityRepository universityRepository;
        private IFacultyRepository facultyRepository;
        private ISpecialityRepository specialityRepository;
        private IDocumentRepository documentRepository;

        public DatabaseContext _context { get; private set; }

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }

                return this.userRepository;
            }
        }

        public IZNORepository ZNORepository
        {
            get
            {
                if (this.znoRepository == null)
                {
                    znoRepository = new ZnoRepository(_context);
                }

                return this.znoRepository;
            }
        }

        public IApplicationRepository ApplicationRepository 
        {
            get
            {
                if (this.applicationRepository == null)
                {
                    this.applicationRepository = new ApplicationRepository(this._context);
                }

                return this.applicationRepository;
            }
        }

        public IFacultyRepository FacultyRepository
        {
            get
            {
                if (this.facultyRepository == null)
                {
                    this.facultyRepository = new FacultyRepository(this._context);
                }

                return this.facultyRepository;
            }
        }

        public ISpecialityRepository SpecialityRepository
        {
            get
            {
                if (this.specialityRepository == null)
                {
                    this.specialityRepository = new SpecialityRepository(this._context);
                }

                return this.specialityRepository;
            }
        }

        public IUniversityRepository UniversityRepository
        {
            get
            {
                if (this.universityRepository == null)
                {
                    this.universityRepository = new UniversityRepository(this._context);
                }

                return this.universityRepository;
            }
        }

        public IDocumentRepository DocumentRepository
        {
            get
            {
                if (this.documentRepository == null)
                {
                    this.documentRepository = new DocumentRepository(this._context);
                }

                return this.documentRepository;
            }
        }

        public Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }
    }
}
