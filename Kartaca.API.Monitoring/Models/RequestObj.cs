using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kartaca.API.Monitoring.Models
{
    public class RequestObj
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string RequestTime { get; set; }
        public string RequestTimeStamp { get; set; }
        public override string ToString()
        {
            return $"{MethodName}-{RequestTime}-{RequestTimeStamp}";
        }
    }
}
