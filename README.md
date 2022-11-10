# HigherLogics.Web.Chartjs

This library is intended to ease the process of defining and initializing Chartjs
charts. Instead of defining charts in HTML markup and JavaScript, you can define
them in C# and this library makes it trivial to embed a script that initializes
the chart:

    var bar = new Chart
    {
        Type = "bar",
        Options = new ChartOptions { Responsive = true, Legend = new ChartLegend { Display = false } },
        Data = new ChartData
        {
            Labels = new[] { "January", "February", "March", "April", "May", "June", "July" },
            Datasets = new[]
            {
                new ChartDataset
                {
                    Label = "Shoes",
                    BackgroundColors = new[] { "#0694a2" },
                    BorderWidth = 1,
                    Data = new object[] { -3, 14, 52, 74, 33, 90, 70 },
                },
                new ChartDataset
                {
                    Label = "Bags",
                    BackgroundColors = new[] { "#7e3af2" },
                    BorderWidth = 1,
                    Data = new object[] { 66, 33, 43, 12, 54, 62, 84 },
                },
            },
        }
    };

If you're using the HigherLogics.Web.Windmill library, then embedding this chart is as simple as
defining the following markup:

    <windmill-chart id="bar" chart="@Model.Bar"></windmill-chart>

If you're using this with raw Razor, then the markup will simply be:

    <canvas id="bar"></canvas>
    @Html.Raw(Model.Bar.ToScript("bar"))

The ToStript(string id) method will generate a script tag with a function that runs when the
"DOMContentLoaded" event fires. It will then lookup the element id for the chart and initialize
it with the data and options that were specified.

# Future Work

  * I'm not sure the current approach will play well with dynamic DOM updates, ie. dynamically loading or updating a chart.