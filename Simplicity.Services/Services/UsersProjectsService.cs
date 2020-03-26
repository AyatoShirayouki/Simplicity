using Simplicity.Entities;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.Services
{
    public class UsersProjectsService : BaseService<UserProject>, IUsersProjectsService
    {
        private readonly IUsersProjectsRepository _usersProjectsRepository;

        public UsersProjectsService(IUsersProjectsRepository usersProjectsRepository) : base(usersProjectsRepository)
        {
            _usersProjectsRepository = usersProjectsRepository;
        }
    }
}
