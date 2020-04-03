﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.User;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    public interface IDocumentService
    {
        Task<bool> Create(Document document );
    }
}
