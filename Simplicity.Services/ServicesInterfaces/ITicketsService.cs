using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface ITicketsService : IBaseService<Ticket>
    {
        List<TaskDto> GetAllTaskDtos(Expression<Func<Ticket, bool>> filter);
    }
}
