using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IDalMetric
    {
        void Add(DalMetricData data);
        List<DalMetricData> GetMetrics();
    }
}
