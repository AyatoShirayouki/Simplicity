using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.Services
{
    public class UserActivityService : IUserActivityService<User>
    {
        public User GetUserActivity()
        {
            return new User();
        }
    }
}
