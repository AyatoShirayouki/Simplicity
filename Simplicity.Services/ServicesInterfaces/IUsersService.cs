using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Users;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IUsersService : IBaseService<User>
    {
        List<UserListDto> GetAllUserDtos(Expression<Func<User, bool>> filter = null);
        List<NameAndIDDto> GetAllUserNameAndIdDtos(Expression<Func<User, bool>> filter);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        void HashUserPassword(UserEditDto user);
        void SaveUser(UserEditDto userEditDto);
        UserEditDto GetUserEditDtoById(int userId);
    }
}
