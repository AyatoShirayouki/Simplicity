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
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(SimplicityContext context) : base(context)
        {

        }

        public UsersRepository()
        {

        }
        public List<UserListDto> GetAllUserDtos(Expression<Func<User, bool>> filter)
        {
            var result = dbSet.Where(filter).Select(u =>
                          new UserListDto
                          {
                              ID = u.ID,
                              Address = u.Address,
                              Name = u.Name,
                              Username = u.Username,
                              Role = new NameAndIDDto { ID = (int)u.Role, Name = u.Role.ToString() },                          
                              Projects = u.UsersProjects.Select(p => new NameAndIDDto
                              {
                                  ID = p.Project.ID,
                                  Name = p.Project.Name
                              }).ToList()
                          }).ToList();

            return result;
        }

        public List<NameAndIDDto> GetAllUserNameAndIdDtos(Expression<Func<User, bool>> filter)
        {
            var result = dbSet.Where(filter).Select(u =>
                          new NameAndIDDto
                          {
                              ID = u.ID,
                              Name = u.Name
                          }).ToList();

            return result;
        }
    }
}
