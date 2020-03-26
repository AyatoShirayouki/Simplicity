using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IGetBaseService<T> where T : BaseEntitity, new()
    {
        T GetById(int id);
    }
}
