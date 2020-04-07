using Application.IServices;
using Domain.Entities;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Services
{
    class DocumentService : IDocumentService
    {
        IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        public async Task<bool> Create(Document document)
        {
            await _unitOfWork.DocumentRepository.CreateAsync(document);
            var saveResult = await _unitOfWork.Commit();

            return saveResult == 1;
        }
    }
}
