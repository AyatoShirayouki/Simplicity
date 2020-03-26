using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using Simplicity.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.Repositories
{
    public class UsersProjectsRepository : BaseRepository<UserProject>, IUsersProjectsRepository
    {
        public UsersProjectsRepository(SimplicityContext context) : base(context)
        {

        }
    }
}
