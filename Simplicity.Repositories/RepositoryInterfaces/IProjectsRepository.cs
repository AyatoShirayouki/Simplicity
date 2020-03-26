using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.RepositoryInterfaces
{
    public interface IProjectsRepository : IBaseRepository<Project>
    {
        List<ProjectDto> GetAllProjectDtos(Expression<Func<Project, bool>> filter);
        List<NameAndIDDto> GetAllProjectNameAndIdDtos(Expression<Func<Project, bool>> filter);
    }
}
