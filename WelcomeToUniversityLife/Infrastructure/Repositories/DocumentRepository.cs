using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class DocumentRepository:Repository<Document,int>,IDocumentRepository
    {
        public DocumentRepository(DatabaseContext context):base(context)
        {
        }
    }
}
