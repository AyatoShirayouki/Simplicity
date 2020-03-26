using Simplicity.DataContracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.ViewModels
{
    public class UsersListVM
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public List<NameAndIDDto> Projects { get; set; }
    }
}
