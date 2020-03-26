using Simplicity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.ViewModels
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Role Role { get; set; }

        public string PicturePath { get; set; }

        public int[] ProjectIDs { get; set; }
    }
}
