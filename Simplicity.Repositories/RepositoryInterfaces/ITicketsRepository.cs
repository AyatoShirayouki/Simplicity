using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.RepositoryInterfaces
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        List<TaskDto> GetAllTaskDtos(Expression<Func<Ticket, bool>> filter);
    }
}
