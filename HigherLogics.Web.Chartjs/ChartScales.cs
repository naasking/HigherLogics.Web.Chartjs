using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HigherLogics.Web.Chartjs
{
    public class ChartScales
    {
        public ChartAxis? X { get; set; }
        public ChartAxis? Y { get; set; }
        internal StringBuilder ToScript(StringBuilder buf)
        {
            buf.Append("scales:{");
            if (X != null)
            {
                buf.Append("x:");
                X?.ToScript(buf);
            }
            if (Y != null)
            {
                buf.Append("y:");
                Y?.ToScript(buf);
            }
            buf.Remove(buf.Length - 1, 1);
            return buf.Append("},");
        }
    }

    public struct ChartAxis
    {
        public bool Display { get; set; }
        public bool DisplayLabel { get; set; }
        public string? Label { get; set; }
        internal StringBuilder ToScript(StringBuilder buf)
        {
            buf.Append('{')
               .Append("display:").Append(Display ? "true" : "false").Append(',');
            if (Label != null)
            {
                buf.Append("scaleLabel:{display:").Append(DisplayLabel ? "true" : "false")
                   .Append(",labelString:'").Append(Label).Append("'},");
            }
            buf.Remove(buf.Length - 1, 1);
            return buf.Append("},");
        }
    }
}
