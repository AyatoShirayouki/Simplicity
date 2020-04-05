using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity
{
    public class MessageHub : Hub<IMessageHubService>
    {
        public void SendMessage(string message)
        {
            this.Clients.All.GetMessage(message);
        }
    }
}
