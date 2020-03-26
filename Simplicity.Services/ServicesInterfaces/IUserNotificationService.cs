using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IUserNotificationService<in T> where T : BaseEntitity
    {
        void SetData(T entity);
    }
}
