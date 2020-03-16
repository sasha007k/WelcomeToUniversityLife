using Domain;
using Domain.IRepositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository userRepository;
        private IZNORepository znoRepository;
        public DatabaseContext _context { get; private set; }

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public Task Commit()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
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

        public IZNORepository ZnoRepository
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
    }
}
