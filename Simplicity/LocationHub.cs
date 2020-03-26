using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity
{
    public class LocationHub : Hub<ILocationHubService>
    {
        public void SendMessage(string message)
        {
            this.Clients.All.GetMessage(message);
        }
    }
}
