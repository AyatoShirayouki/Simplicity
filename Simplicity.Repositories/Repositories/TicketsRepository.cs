using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using Simplicity.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.Repositories
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(SimplicityContext context) : base(context)
        {

        }
        public List<TaskDto> GetAllTaskDtos(Expression<Func<Ticket, bool>> filter)
        {
            var result = dbSet.Where(filter).Select(u =>
                          new TaskDto
                          {
                              ID = u.ID,
                              Name = u.Name,
                              Description = u.Description,
                              Assignee =  new NameAndIDDto { ID = u.AssigneeID, Name = u.Assignee.Name },
                              Creator = new NameAndIDDto { ID = u.CreatorID, Name = u.Creator.Name },
                              Project = new NameAndIDDto { ID = u.ProjectID, Name = u.Project.Name },
                              DueDate = u.DueDate,
                              Status = u.Status,
                              OldStatus = u.Status,
                              IsExpiring = (u.DueDate - DateTime.Now).TotalDays < 4 && DateTime.Now < u.DueDate,
                              IsExpired = DateTime.Now > u.DueDate
                          }).ToList();
            return result;
        }
    }
}
