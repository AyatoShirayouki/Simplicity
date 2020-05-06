using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity
{
    public interface IMessageHubService
    {
        Task GetMessage(string lat);
        //TO DO must return task
        //void SendToUser(string userId, string message);
        //void SendToAll(string message);
    }   
}
