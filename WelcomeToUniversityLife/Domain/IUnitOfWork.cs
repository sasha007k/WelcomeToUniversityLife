using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
