﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
