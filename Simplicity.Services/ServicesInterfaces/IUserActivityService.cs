using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IUserActivityService<out T> where T : BaseEntitity
    {
        T GetUserActivity();
    }
}
