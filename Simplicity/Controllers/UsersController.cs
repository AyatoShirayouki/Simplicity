using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var userDto = _usersService.GetAllUserDtos(x => x.ID == id).FirstOrDefault();

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
            
            var entity = new User();
            _mapper.Map(model, entity);

            if (file != null)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                var fileHelper = new FileHelper();
                entity.PicturePath = fileHelper.SaveFile(fileBytes, entity.Username, file.FileName);
            }

            if (!_usersService.Save(entity))
                return StatusCode(500);

            return Ok(entity);

        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isUserDeleted = _usersService.Delete(id);
            if (!isUserDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
