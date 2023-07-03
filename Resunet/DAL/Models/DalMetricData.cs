namespace Estore.DAL.Models
{
    public class DalMetricData
    {
        public string Sql { get; set; } = null!;
        public string Parameters { get; set; } = null!;
        public TimeSpan Elapsed { get; set; }
    }
}
