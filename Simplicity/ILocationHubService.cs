using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity
{
    public interface ILocationHubService
    {
        Task GetMessage(string lat);
    }   
}
