using AutoMapper;
using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Projects;
using Simplicity.DataContracts.Dtos.Tasks;
using Simplicity.DataContracts.Dtos.Users;
using Simplicity.Entities;
using Simplicity.ViewModels;
using Simplicity.ViewModels.Projects;
using Simplicity.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.Mappings
{
    public class AutomapperMappings : Profile
    {
        public AutomapperMappings()
        {
            CreateMap<UsersListVM, UserListDto>();
            CreateMap<User, UserDto>();
            CreateMap<UsersEditVM, UserEditDto>();
            CreateMap<User, UserEditDto>().ForMember(x=>x.Password, opt=>opt.Ignore());
            CreateMap<UserEditDto, User>();
            CreateMap<User, UsersListVM>();
            CreateMap<User, UserListDto>().ForMember(u=>u.Role, opt => opt.Ignore());

            CreateMap<TasksEditVM, TaskEditDto>();
            CreateMap<TaskEditDto, Ticket>();
            CreateMap<Ticket, TaskDto>()
                    .ForPath(t=>t.OldStatus, opt=>opt.Ignore())
                    .ForPath(t => t.IsExpired, opt => opt.Ignore())
                    .ForPath(t => t.IsExpiring, opt => opt.Ignore());

            CreateMap<ProjectsListVM, ProjectDto>();
            CreateMap<ProjectsEditVM, ProjectEditDto>();
            CreateMap<ProjectEditDto, Project>().ForMember(u => u.UsersProjects, opt => opt.Ignore());
            CreateMap<Project, ProjectDto>().ForPath(u => u.AssignedUsers, opt => opt.Ignore());

        }
    }
}