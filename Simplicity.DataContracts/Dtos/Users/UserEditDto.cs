using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.DataContracts.Dtos.Users
{
    public class UserEditDto
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Role Role { get; set; }

        public string PicturePath { get; set; }
    }
}
