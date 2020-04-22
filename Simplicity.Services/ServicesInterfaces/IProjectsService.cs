using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Projects;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IProjectsService : IBaseService<Project>
    {
        List<ProjectDto> GetAllProjectDtos(Expression<Func<Project, bool>> filter = null);

        List<NameAndIDDto> GetAllProjectNameAndIdDtos(Expression<Func<Project, bool>> filter);

        bool AssignUsers(int projectID, int[] userIDs);

        void SaveProject(ProjectEditDto projectEditDto, int[] assignedUsers);

        ProjectDto GetDtoById(int id);
    }
}
