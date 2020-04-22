﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITicketsService _tasksService;
        private readonly IMapper _mapper;
        private IHubContext<MessageHub, IMessageHubService> _hubContext;
        private readonly IUsersService _usersService;

        public TasksController(ITicketsService tasksService,
            IMapper mapper, IHubContext<MessageHub, IMessageHubService> hubContext,
            IUsersService usersService)
        {
            _tasksService = tasksService;
            _mapper = mapper;
            _hubContext = hubContext;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _tasksService.GetAllTaskDtos(x => true);
            return Ok(result);
        }

        [HttpGet("getByUserID/{id}")]
        public IActionResult GetByUserID(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var result = _tasksService.GetAllTaskDtos(x => x.AssigneeID == id);
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

            var changes = PrepareChanges(model);

            //map users to projects here
            _tasksService.SaveTicket(entity);

            _hubContext.Clients.All.GetMessage(changes);
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
            _hubContext.Clients.All.GetMessage($"{ticketName} was deleted");
            
            return Ok();
        }

        //move to service
        //rename to PrepareStatusChange
        private string PrepareChanges(TasksEditVM model)
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