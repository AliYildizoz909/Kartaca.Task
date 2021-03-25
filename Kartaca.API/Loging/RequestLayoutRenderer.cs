using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;

namespace Kartaca.API.Loging
{
    [LayoutRenderer("request-times")]
    public class RequestLayoutRenderer: LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append("");
        }
    }
}
