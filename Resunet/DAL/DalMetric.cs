using Estore.DAL.Models;

namespace Estore.DAL
{
    public class DalMetric : IDalMetric
    {
        private readonly List<DalMetricData> _metrics = new();
        public void Add(DalMetricData data)
        {
            _metrics.Add(data);
        }

        public List<DalMetricData> GetMetrics()
        {
            return _metrics;
        }
    }
}
