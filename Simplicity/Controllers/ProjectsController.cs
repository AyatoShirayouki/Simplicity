using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplicity.DataContracts.Dtos.Projects;
using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using Simplicity.ViewModels.Projects;

namespace Simplicity.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IUsersProjectsService _usersProjectsService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectsService projectsService, 
            IUsersProjectsService usersProjectsService,
            IMapper mapper)
        {
            _projectsService = projectsService;
            _usersProjectsService = usersProjectsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize("AdminMod")]

        public IActionResult Get()
        {
            var result = _projectsService.GetAllProjectDtos(x=> true);
            return Ok(result);
        }

        [HttpGet("getProjectNameAndIds")]
        [Authorize("AdminMod")]
        public IActionResult GetProjectNameAndIds()
        {
            var result = _projectsService.GetAllProjectNameAndIdDtos(x => true);
            return Ok(result);
        }


        [HttpGet("getProjectByUserId")]
        public IActionResult GetProjectByUserId(int id)
        {
            var result = _projectsService.GetAllProjectNameAndIdDtos(x => x.UsersProjects.Any(u => u.UserID == id));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "projects/getByID")]
        [Authorize("AdminMod")]
        public IActionResult GetByID(int id)
        {
            var model = _projectsService.GetAllProjectDtos(x => x.ID == id).FirstOrDefault();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
        
        [HttpPost]
        [Authorize("AdminMod")]
        public IActionResult Post([FromForm] ProjectsEditVM model)
        {
            if (model == null)
            {
                return NotFound();
            }

            model.AssignedUsers = new int[0];

            if (!string.IsNullOrEmpty(model.AssignedUsersAsString))
            {
                model.AssignedUsers = model.AssignedUsersAsString.Split(",").Select(x => int.Parse(x)).ToArray();
            }

            var projectEditDto = new ProjectEditDto();
            _mapper.Map(model, projectEditDto);
            
            _projectsService.SaveProject(projectEditDto, model.AssignedUsers);
            
            var projectEditVM = _projectsService.GetAllProjectDtos(x => x.ID == projectEditDto.ID).FirstOrDefault();

            return Ok(projectEditVM);
        }
        
        [HttpPost("{id}", Name = "projects/assignUsers")]
        [Authorize("AdminMod")]
        public IActionResult AssingUsers(int projectID, int[] userIDs)
        {
            //map users to projects here
            if (!_projectsService.AssignUsers(projectID, userIDs))
                return StatusCode(500);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize("AdminMod")]
        public IActionResult Delete(int id)
        {
            if (_projectsService.GetById(id) == null)
                return NotFound();

             _projectsService.Delete(id);

            return Ok();
        }

    }
}