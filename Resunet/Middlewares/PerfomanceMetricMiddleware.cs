using Estore.DAL;
using Estore.DAL.Models;
using System.Text;

namespace Estore.Middlewares
{
    public class PerfomanceMetricMiddleware
    {
        private readonly RequestDelegate _next;

        public PerfomanceMetricMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDalMetric dalMetric)
        {
            await _next.Invoke(context);

            var metrics = dalMetric.GetMetrics();
            if (!metrics.Any()) return;
            var html = MetricsHtml(metrics);
            await context.Response.WriteAsync(html);
        }

        private string MetricsHtml(List<DalMetricData> data)
        {
            int index = 1;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<table class'table'>");
            foreach (var metric in data)
            {
                stringBuilder.Append($"<tr><td>{index}</td><td>{metric.Sql}</td><td>{metric.Parameters}</td><td>{metric.Elapsed}</td></tr>");
                index++;
            }
            stringBuilder.Append("</table>");
            return $"<div class='dev-performance'>{stringBuilder}</div>";
        }
    }

    public static class MetricExtensions
    {
        public static IApplicationBuilder UseDalPerfomanceMetric(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PerfomanceMetricMiddleware>();
            return builder;
        }
    }
}
