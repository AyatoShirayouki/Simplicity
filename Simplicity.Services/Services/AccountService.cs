using Microsoft.Extensions.Configuration;
using Simplicity.DataContracts;
using Simplicity.Entities;
using Simplicity.Services.ServicesInterfaces;
using System;
using Microsoft.IdentityModel.Tokens;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Simplicity.Helpers;
using AutoMapper;
using Simplicity.DataContracts.Dtos;
using Simplicity.DataContracts.Dtos.Users;

namespace Simplicity.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUsersService _usersService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public AccountService(IUsersService usersService, AppSettings appSettings,
            IMapper mapper)
        {
            _usersService = usersService;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public UserEditDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _usersService.GetAll().SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!_usersService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var userDto = new UserEditDto();
            
            _mapper.Map(user, userDto);
            // authentication successful
            return userDto;
        }

        public async Task<string> CreateToken(UserEditDto user, IOptions<AppSettings> appSettings)
        {
            var utcNow = DateTime.UtcNow;
            var config = appSettings.Value;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                        new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.Secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(appSettings.Value.Issuer, appSettings.Value.Audience, claims, expires: utcNow.AddSeconds(double.Parse(appSettings.Value.Lifetime)), signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(jwt);

            return tokenString;
        }

        public ClaimsPrincipal ValidateToken(string authToken)
        {
            try
            {
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = _appSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = _appSettings.Issuer,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                };

                return tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
