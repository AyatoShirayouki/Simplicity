using Microsoft.Extensions.Options;
using Simplicity.DataContracts.Dtos;
using Simplicity.Entities;
using Simplicity.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Simplicity.Services.ServicesInterfaces
{
    public interface IAccountService
    {
        UserListDto Authenticate(string username, string password);
        Task<string> CreateToken(UserListDto user, IOptions<AppSettings> appSettings);
        ClaimsPrincipal ValidateToken(string authToken);
    }
}
