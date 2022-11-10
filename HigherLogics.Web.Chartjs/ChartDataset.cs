using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace HigherLogics.Web.Chartjs
{
    /// <summary>
    /// The dataset for a specific chart.
    /// </summary>
    public class ChartDataset
    {
        public string? Type { get; set; }
        public string? Clip { get; set; }
        public int? Order { get; set; }
        public string? Stack { get; set; }
        public object? Parsing { get; set; }
        public string? Label { get; set; }
        public bool Hidden { get; set; }
        public IEnumerable<string>? BackgroundColors { get; set; }
        public IEnumerable<string>? BorderColors { get; set; }
        public int? BorderWidth { get; set; }
        public bool? ShowLine { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public bool? Fill { get; set; }

        //FIXME: many more properties are possible:
        //https://stackoverflow.com/questions/46185159/chart-js-using-json-data

        internal StringBuilder ToScript(StringBuilder buf)
        {
            var length = buf.Append('{').Length;
            if (!string.IsNullOrEmpty(Type))
                buf.Append("type:'").Append(Type).AppendLine("',");
            if (!string.IsNullOrEmpty(Clip))
                buf.Append("clip:'").Append(Clip).AppendLine("',");
            if (!string.IsNullOrEmpty(Stack))
                buf.Append("stack:'").Append(Stack).AppendLine("',");
            if (!string.IsNullOrEmpty(Label))
                buf.Append("label:'").Append(Label).AppendLine("',");
            if (Hidden)
                buf.AppendLine("hidden:true,");
            if (Order != null)
                buf.Append("order:").Append(Order.Value).Append(',');
            if (Fill != null)
                buf.Append("fill:").Append(Fill.Value ? "true" : "false").Append(',');
            if (BorderWidth != null)
                buf.Append("borderWidth:").Append(BorderWidth.Value).Append(',');
            if (ShowLine != null)
                buf.Append("showLine:").Append(ShowLine.Value).Append(',');
            if (BorderColors != null)
            {
                if (BorderColors.Count() == 1)
                {
                    buf.Append("borderColor:'").Append(BorderColors.Single()).Append("',");
                }
                else
                {
                    var mark = buf.Append("backgroundColor:[").Length;
                    foreach (var x in BorderColors)
                        buf.Append('\'').Append(x).Append("',");
                    // remove trailing comma
                    if (buf.Length > mark)
                        buf.Remove(buf.Length - 1, 1);
                    buf.AppendLine("],");
                }
            }
            if (BackgroundColors != null)
            {
                if (BackgroundColors.Count() == 1)
                {
                    buf.Append("backgroundColor:'").Append(BackgroundColors.Single()).Append("',");
                }
                else
                {
                    var mark = buf.Append("backgroundColor:[").Length;
                    foreach (var x in BackgroundColors)
                        buf.Append('\'').Append(x).Append("',");
                    // remove trailing comma
                    if (buf.Length > mark)
                        buf.Remove(buf.Length - 1, 1);
                    buf.AppendLine("],");
                }
            }
            if (Data != null)
            {
                buf.Append("data:Object.values(")
                   .Append(JsonSerializer.Serialize(Data))
                   .AppendLine("),");
            }

            // remove trailing comma
            if (buf.Length > length)
                buf.Remove(buf.Length - 1, 1);
            return buf.Append('}');
        }
    }
}
