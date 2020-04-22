using AutoMapper;
using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Projects;
using Simplicity.Entities;
using Simplicity.Repositories.Repositories;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Services.Services
{
    public class ProjectsService : BaseService<Project>, IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IUsersProjectsRepository _usersProjectsRepository;
        private readonly IMapper _mapper;

        public ProjectsService(IProjectsRepository projectsRepository,
                IUsersProjectsRepository usersProjectsRepository,
                IMapper mapper) 
            : base(projectsRepository)
        {
            _projectsRepository = projectsRepository;
            _usersProjectsRepository = usersProjectsRepository;
            _mapper = mapper;
        }

        public bool AssignUsers(int projectID, int[] userIDs)
        {
            var userProjects = _usersProjectsRepository.GetAll(up => up.ProjectID == projectID);
            var userProjectsArray = _usersProjectsRepository.GetAll(up => up.ProjectID == projectID).Select(x=>x.UserID).ToArray();

            if (userProjects == null)
                return false;

            if (userProjects == null)
            {
                userProjects = new List<UserProject>();
            }

            var toBeRemoved = userProjects.Where(x => !userIDs.Contains(x.UserID)).ToList() ?? new List<UserProject>();
            var toBeAdded = userIDs.Where(x => !userProjectsArray.Contains(x));

            foreach (var id in toBeAdded)
            {
                if (!userProjects.Any(x=>x.UserID == id))
                {
                    userProjects.Add(new UserProject
                    {
                        ProjectID = projectID,
                        UserID = id
                    }); 

                }
            }

            foreach (var item in toBeRemoved)
            {
                userProjects.Remove(item);
                _usersProjectsRepository.Delete(item.ID);
            }

            foreach (var item in userProjects)
            {
                _usersProjectsRepository.Save(item);
            }
            return true;
        }

        public List<ProjectDto> GetAllProjectDtos(Expression<Func<Project, bool>> filter = null)
        {
            return _projectsRepository.GetAllProjectDtos(filter);
        }

        public List<NameAndIDDto> GetAllProjectNameAndIdDtos(Expression<Func<Project, bool>> filter)
        {
            return _projectsRepository.GetAllProjectNameAndIdDtos(filter);
        }

        public void SaveProject(ProjectEditDto projectEditDto, int[] assignedUsers)
        {
            var entity = new Project();

            if (projectEditDto.ID !=0)
            {
                entity = GetById(projectEditDto.ID);
            }

            entity.Name = projectEditDto.Name;
            entity.FromDate = projectEditDto.FromDate;
            entity.ToDate = projectEditDto.ToDate;
            
            this.Save(entity);

            this.AssignUsers(entity.ID, assignedUsers);
        }

        public ProjectDto GetDtoById(int id)
        {
            var project = this.GetById(id);
            return _mapper.Map(project, new ProjectDto());
        }
    }
}
