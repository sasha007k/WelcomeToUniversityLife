using Domain.Entities;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IDocumentService
    {
        Task<bool> Create(Document document);
    }
}