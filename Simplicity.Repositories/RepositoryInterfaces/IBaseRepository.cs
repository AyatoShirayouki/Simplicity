using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.RepositoryInterfaces
{
    public interface IBaseRepository<T> where T : BaseEntitity
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        T GetById(int id);

        void Save(T item);

        void Delete(int id);
    }
}
