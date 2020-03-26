using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using Simplicity.Repositories.Repositories;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplicity.Services.Services
{
    public class UsersService : BaseService<User>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) : base(usersRepository)
        {
            _usersRepository = usersRepository;
        }

        List<UserDto> IUsersService.GetAllUserDtos(Expression<Func<User, bool>> filter)
        {
            IBaseRepository<User> repo = new UsersRepository();

            return _usersRepository.GetAllUserDtos(filter);
        }

        List<NameAndIDDto> IUsersService.GetAllUserNameAndIdDtos(Expression<Func<User, bool>> filter)
        {
            return _usersRepository.GetAllUserNameAndIdDtos(filter);
        }

        public override void OnBeforeAdd(User item)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(item.Password, out passwordHash, out passwordSalt);

            item.PasswordHash = passwordHash;
            item.PasswordSalt = passwordSalt;

        }

        public override void OnBeforeEdit(User item)
        {
            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(item.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(item.Password, out passwordHash, out passwordSalt);

                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private void CovarianceExample()
        {
            IUserActivityService<BaseEntitity> userActivityService = new UserActivityService();
            var userActivity = userActivityService.GetUserActivity();
        }

        private void ContravarianceExample()
        {
            IUserNotificationService<User> service = new UserNotificationService();
            service.SetData(new User());
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
