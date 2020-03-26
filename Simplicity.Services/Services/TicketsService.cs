using Simplicity.DataContracts.Dtos;
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

        public TicketsService(ITicketsRepository ticketsRepository) : base(ticketsRepository)
        {
            _ticketsRepo = ticketsRepository;
        }

        public List<TaskDto> GetAllTaskDtos(Expression<Func<Ticket, bool>> filter)
        {
            return _ticketsRepo.GetAllTaskDtos(filter);
        }
    }
}
