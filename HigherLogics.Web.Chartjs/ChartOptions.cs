using System;
using System.Collections.Generic;
using System.Text;

namespace HigherLogics.Web.Chartjs
{
    public class ChartOptions
    {
        public IEnumerable<string>? Events { get; set; }
        public ChartLegend? Legend { get; set; }
        public bool Responsive { get; set; }
        public int? CutoutPercentage { get; set; }
        public ChartPlugin Tooltips { get; set; }
        public ChartPlugin Hover { get; set; }
        public ChartScales? Scales { get; set; }

        internal StringBuilder ToScript(StringBuilder buf)
        {
            buf.Append('{');
            buf.Append("responsive:").Append(Responsive ? "true" : "false").Append(',');
            if (CutoutPercentage != null)
                buf.Append("cutoutPercentage:").Append(CutoutPercentage.Value).Append(',');
            if (Legend != null)
            {
                buf.Append("legend:");
                Legend.Value.ToScript(buf).Append(',');
            }
            Scales?.ToScript(buf);

            // remove trailing comma
            buf.Remove(buf.Length - 1, 1);
            return buf.Append('}');
        }
    }
}
