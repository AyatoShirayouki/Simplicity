using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IBaseService<T> where T : BaseEntitity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        //T GetById(int id);
        
        bool Delete(int id);

        bool Save(T item);
    }
}
