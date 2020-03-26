using AutoMapper;
using Simplicity.DataContracts.Dtos;
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
            CreateMap<UsersListVM, UserDto>();
            CreateMap<UsersEditVM, User>().ForMember(u=>u.UsersProjects, opt=>opt.Ignore());

            CreateMap<TasksEditVM, Ticket>();


            CreateMap<ProjectsListVM, ProjectDto>();
            CreateMap<ProjectsEditVM, Project>().ForMember(u => u.UsersProjects, opt => opt.Ignore());
        }
    }
}
