using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.Services
{
    public class UserNotificationService : IUserNotificationService<BaseEntitity>
    {
        public void SetData(BaseEntitity entitity)
        {
            entitity.ID = 5;
        }
    }
}
