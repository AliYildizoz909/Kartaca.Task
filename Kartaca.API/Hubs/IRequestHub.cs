using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kartaca.API.Monitoring.Models;

namespace Kartaca.API.Hubs
{
    public interface IRequestHub
    {
        Task ReceiveRequest(RequestObj obj);
        Task ReceiveAllRequest(List<RequestObj> list);
    }
}
