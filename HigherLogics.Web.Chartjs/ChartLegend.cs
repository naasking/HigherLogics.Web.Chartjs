using System;
using System.Collections.Generic;
using System.Text;

namespace HigherLogics.Web.Chartjs
{
    public struct ChartLegend
    {
        public bool Display { get; set; }
        internal StringBuilder ToScript(StringBuilder buf)
        {
            buf.Append('{');
            buf.Append("display:").Append(Display ? "true" : "false").Append(',');
            buf.Remove(buf.Length - 1, 1);
            return buf.Append('}');
        }
    }
}
