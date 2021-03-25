using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kartaca.API.Monitoring.Models;
using Microsoft.AspNetCore.SignalR;

namespace Kartaca.API.Hubs
{
    public class RequestHub : Hub<IRequestHub>
    {
        public static List<RequestObj> Requests { get; set; } = new List<RequestObj>();
        public async Task SendRequest(RequestObj obj)
        {
            RequestHub.Requests.Add(obj);
            await Clients.All.ReceiveRequest(obj);
        }
        public async Task GetAllRequest()
        {
            await Clients.Caller.ReceiveAllRequest(Requests);
        }
    }
}
