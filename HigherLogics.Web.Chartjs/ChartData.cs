using System;
using System.Collections.Generic;
using System.Text;

namespace HigherLogics.Web.Chartjs
{
    /// <summary>
    /// The data for a chart.
    /// </summary>
    public struct ChartData
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<ChartDataset> Datasets { get; set; }
        internal StringBuilder ToScript(StringBuilder buf)
        {
            var length = buf.Append('{').Length;
            if (Labels != null)
            {
                //FIXME: use object.keys() from the dataset of labels not explicitly set?
                buf.Append("labels:[");
                foreach (var x in Labels)
                    buf.Append('\'').Append(x).Append("',");
                buf.Append("],");
            }
            if (Datasets != null)
            {
                var mark = buf.Append("datasets:[").Length;
                foreach (var x in Datasets)
                    x.ToScript(buf).AppendLine(",");
                if (buf.Length > mark)
                    buf.Remove(buf.Length - 1, 1);
                buf.AppendLine("],");
            }
            // remove trailing comma
            if (buf.Length > length)
                buf.Remove(buf.Length - 1, 1);
            return buf.Append('}');
        }
    }
}
