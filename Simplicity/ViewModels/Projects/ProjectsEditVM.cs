using Simplicity.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.ViewModels.Projects
{
    public class ProjectsEditVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<ISelectItem> AssignedUsersList { get; set; }

        public int[]  AssignedUsers { get; set; }

        public string AssignedUsersAsString { get; set; }
    }
}
