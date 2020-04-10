using System.Threading.Tasks;
using Domain.Entities;

namespace Application.IServices
{
    public interface IDocumentService
    {
        Task<bool> Create(Document document);
    }
}