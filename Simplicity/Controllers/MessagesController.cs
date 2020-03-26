
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Simplicity;

namespace SignalRApi.Controllers
{
    [Produces("application/json")]
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private IHubContext<LocationHub, ILocationHubService> _hubContext;

        public LocationController(IHubContext<LocationHub, ILocationHubService> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post([FromBody]string message)
        {
            string retMessage = string.Empty;

            try
            {
                //_hubContext.Clients.All.GetMessage(message, "this is a message");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}
