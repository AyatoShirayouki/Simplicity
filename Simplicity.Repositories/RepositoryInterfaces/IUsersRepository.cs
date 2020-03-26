using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.RepositoryInterfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        List<UserDto> GetAllUserDtos(Expression<Func<User, bool>> filter = null);
        List<NameAndIDDto> GetAllUserNameAndIdDtos(Expression<Func<User, bool>> filter);
    }
}
