using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.User;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    interface IDocumentService
    {
        Task<bool> Craate(Document document );
    }
}
