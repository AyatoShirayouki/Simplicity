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
    public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository
    {
        public ProjectsRepository(SimplicityContext context) : base(context)
        {

        }
        public List<ProjectDto> GetAllProjectDtos(Expression<Func<Project, bool>> filter)
        {
            return dbSet.Select(p => new ProjectDto
            {
                ID = p.ID,
                FromDate = p.FromDate,
                ToDate = p.ToDate,
                Name = p.Name,
                AssignedUsers = p.UsersProjects.Select(pu => new NameAndIDDto
                {
                    ID = pu.UserID,
                    Name = pu.User.Name

                }).ToList()
            }).ToList();

        }

        public List<NameAndIDDto> GetAllProjectNameAndIdDtos(Expression<Func<Project, bool>> filter)
        {
            return dbSet.Where(filter).Select(p => new NameAndIDDto
            {
                ID = p.ID,
                Name = p.Name,
            }).ToList();

        }
    }
}
