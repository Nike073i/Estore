using Estore.DAL.Models;

namespace Estore.DAL
{
    public class DalMetricStub : IDalMetric
    {
        public void Add(DalMetricData data)
        {
            return;
        }

        public List<DalMetricData> GetMetrics()
        {
            return new();
        }
    }
}
