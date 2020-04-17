﻿using Application.IServices;
using Domain;
using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Create(Document document)
        {

            await _unitOfWork.DocumentRepository.CreateAsync(document);
            var saveResult = await _unitOfWork.Commit();

            return saveResult == 1;
        }
    }
}