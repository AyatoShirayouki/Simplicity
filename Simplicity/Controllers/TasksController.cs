using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Simplicity.DataContracts.Dtos.Tasks;
using Simplicity.Entities;
using Simplicity.Helpers;
using Simplicity.Services.ServicesInterfaces;
using Simplicity.ViewModels.Tasks;

namespace Simplicity.Controllers
{
    [Route("api/tasks")]
    //[Authorize]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITicketsService _tasksService;
        private readonly IMapper _mapper;
        //private readonly IMessageHubService _messageHubService;
        private IHubContext<MessageHub, IMessageHubService> _messageHubService;
        private readonly IUsersService _usersService;

        public TasksController(ITicketsService tasksService,
            IMapper mapper, IHubContext<MessageHub, IMessageHubService> hubContext,
            IUsersService usersService)
        {
            _tasksService = tasksService;
            _mapper = mapper;
            _messageHubService = hubContext;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _tasksService.GetAllTaskDtos(x => true);
            throw new Exception();
            return Ok(result);
        }

        [HttpGet("getByUserID/{id}")]
        public IActionResult GetByUserID(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _tasksService.GetAllTaskDtos(x => x.AssigneeID == id || x.CreatorID == id);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "tasks/getByID")]
        public IActionResult GetByID(int id)
        {
            var model = _tasksService.GetTaskDtoById(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post([FromForm] TasksEditVM model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var entity = new TaskEditDto();
            _mapper.Map(model, entity);

            var changes = PrepareStatusChange(model);
            
            _tasksService.SaveTicket(entity);

            _messageHubService.Clients.All.GetMessage(changes);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _tasksService.GetTaskDtoById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var ticketName = ticket.Name;

            _tasksService.Delete(id);
            var userIDAsString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _messageHubService.Clients.User(userIDAsString).GetMessage($"{ticketName} was deleted");
            //_hubContext.Clients.All.GetMessage($"{ticketName} was deleted");

            return Ok();
        }
        
        private string PrepareStatusChange(TasksEditVM model)
        {
            var action = "was updated";

            if (model.Status.ToString() != model.OldStatus.ToString())
            {
                action = $@"updated status from {model.OldStatus.GetDescription()}
                            to {model.Status.GetDescription()}";
            }
            
            return $"'{model.Name}' {action}";
        }
    }
}