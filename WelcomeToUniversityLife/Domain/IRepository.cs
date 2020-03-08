using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntityBase
    {

    }
}
