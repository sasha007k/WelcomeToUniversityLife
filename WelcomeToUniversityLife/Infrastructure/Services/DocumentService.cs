using Application.IServices;
using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    class DocumentService : IDocumentService
    {
        DatabaseContext _dbContext;

        public DocumentService(DatabaseContext context)
        {
            this._dbContext = context;

        }
        public async Task<bool> Create(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            var saveResult = await _dbContext.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
