using AutoMapper;
using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Tasks;
using Simplicity.Entities;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Services.Services
{
    public class TicketsService : BaseService<Ticket>, ITicketsService
    {
        private readonly ITicketsRepository _ticketsRepo;
        private readonly IMapper _mapper;

        public TicketsService(ITicketsRepository ticketsRepository,
            IMapper mapper) : base(ticketsRepository)
        {
            _ticketsRepo = ticketsRepository;
            _mapper = mapper;
        }

        public List<TaskDto> GetAllTaskDtos(Expression<Func<Ticket, bool>> filter)
        {
            return _ticketsRepo.GetAllTaskDtos(filter);
        }

        public void SaveTicket(TaskEditDto taskDto)
        {
            var entity = new Ticket();
            _mapper.Map(taskDto, entity);
            this.Save(entity);
        }
    }
}
