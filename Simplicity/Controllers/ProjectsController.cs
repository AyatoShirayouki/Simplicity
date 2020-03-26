using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using Simplicity.ViewModels.Projects;

namespace Simplicity.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize("AdminMod")]
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
        public IActionResult Get()
        {
            var result = _projectsService.GetAllProjectDtos(x=> true);
            return Ok(result);
        }

        [HttpGet("getProjectNameAndIds")]
        public IActionResult GetProjectNameAndIds()
        {
            var result = _projectsService.GetAllProjectNameAndIdDtos(x => true);
            return Ok(result);
        }


        [HttpGet("getProjectByUserId")]
        public IActionResult GetProjecstByUserId(int id)
        {
            var result = _projectsService.GetAllProjectNameAndIdDtos(x => x.UsersProjects.Any(u => u.UserID == id));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "projects/getByID")]
        public IActionResult GetByID(int id)
        {
            var model = _projectsService.GetAllProjectDtos(x => x.ID == id).FirstOrDefault();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
        
        [HttpPost]
        public IActionResult Post([FromForm] ProjectsEditVM model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            model.AssignedUsers = model.AssignedUsersAsString.Split(",").Select(x=>int.Parse(x)).ToArray();
            var entity = new Project();
            _mapper.Map(model, entity);

            //map users to projects here
            if (!_projectsService.Save(entity))
                return StatusCode(500);
            if (!_projectsService.AssignUsers(entity.ID, model.AssignedUsers))
            {
                return StatusCode(500);

            }
            var projectEditVM = _projectsService.GetAllProjectDtos(x => x.ID == entity.ID).FirstOrDefault();

            return Ok(projectEditVM);
        }
        
        [HttpPost("{id}", Name = "projects/assignUsers")]
        public IActionResult AssingUsers(int projectID, int[] userIDs)
        {
            //map users to projects here
            if (!_projectsService.AssignUsers(projectID, userIDs))
                return StatusCode(500);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isProjectDeleted = _projectsService.Delete(id);
            if (!isProjectDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}