using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kartaca.API.Monitoring.Models
{
    public class RequestObj
    {
        public RequestObj()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public double RequestTime { get; set; }
        public DateTime RequestTimeStamp { get; set; }
        public override string ToString()
        {
            return $"{MethodName}-{RequestTime}-{RequestTimeStamp}";
        }
    }
}
