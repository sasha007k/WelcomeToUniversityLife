using Domain;

namespace Infrastructure
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        public readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
