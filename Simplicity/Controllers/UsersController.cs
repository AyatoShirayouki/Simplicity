using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplicity.DataContracts.Dtos.Users;
using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using Simplicity.ViewModels;

namespace Simplicity.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize("Administrator")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService,
            IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _usersService.GetAllUserDtos(x => true);
            return Ok(result);
        }

        [HttpGet("getUserNameAndIds")]
        [AllowAnonymous]
        public IActionResult GetUserNameAndIds()
        {
            var result = _usersService.GetAllUserNameAndIdDtos(x => true);
            return Ok(result);
        }

        [HttpGet("getUserTeammates")]
        [AllowAnonymous]
        public IActionResult GetUserTeammates(int userId)
        {
            var result = _usersService.GetAllUserNameAndIdDtos(x => x.UsersProjects.Any(u => u.UserID == userId));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetByID")]
        public IActionResult GetByID(int id)
        {
            var userDto = _usersService.GetUserListDtoById(id);

            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult Post([FromForm] UsersEditVM model, IFormFile file)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var entity = new UserEditDto();
            _mapper.Map(model, entity);
            _usersService.HashUserPassword(entity);

            _usersService.SaveUser(entity);

            return Ok(entity);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_usersService.GetById(id) == null)
                return NotFound();

            _usersService.Delete(id);

            return Ok();
        }
    }
}
