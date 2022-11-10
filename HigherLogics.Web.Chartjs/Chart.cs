using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace HigherLogics.Web.Chartjs
{
    /// <summary>
    /// A description of a chart.
    /// </summary>
    public struct Chart
    {
        /// <summary>
        /// The type of chart to create.
        /// </summary>
        public string Type { get; set; }
        public ChartData Data { get; set; }
        public ChartOptions Options { get; set; }
        public IEnumerable<string> Plugins { get; set; }

        /// <summary>
        /// Create a script for the document that initializes a chart with data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ToScript(string id)
        {
            var buf = new StringBuilder("<script>document.addEventListener('DOMContentLoaded', function() {").AppendLine();
            buf.Append("var e=document.getElementById('").Append(id).AppendLine("');");
            buf.AppendLine("var c=new Chart(e, {");
            buf.Append("type:'").Append(Type).AppendLine("',");
            buf.Append("data:");
            Data.ToScript(buf).Append(',');

            buf.Append("options:");
            if (Options == null)
                buf.Append(GetDefaultOptions());
            else
                Options.ToScript(buf).AppendLine(",");

            // remove trailing comma
            buf.Remove(buf.Length - 1, 1);
            buf.Append("}); window['chart-'+").Append(id).AppendLine("]=c;");
            return buf.AppendLine("});</script>").ToString();
        }

        StringBuilder Property(StringBuilder buf, string name, string value) =>
            buf.Append(name).Append(':').Append(value).Append(',');

        string GetDefaultOptions()
        {
            switch (Type)
            {
                case "pie":
                    return "{responsive:true,cutoutPercentage:80,legend:{display:false}}";
                case "lines":
                    return "{responsive:true,legend:{display:false,},tooltips:{mode:'index',intersect: false,},hover:{mode:'nearest',intersect:true},scales:{x:{display:true,scaleLabel:{display:true,labelString:'X'}},y:{display:true,scaleLabel:{display:true,labelString:'Y'}}}}";
                case "bars":
                    return "{responsive:true,legend:{display:false}}";
                default:
                    return "{}";
            }
        }
    }
}
