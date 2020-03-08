using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntityBase
    {
        public readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
