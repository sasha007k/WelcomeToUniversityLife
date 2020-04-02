using Domain.Entities;
using Application.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Application.Services
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
