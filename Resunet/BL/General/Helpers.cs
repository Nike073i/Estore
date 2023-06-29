using System.Transactions;

namespace Estore.BL.General
{
    public static class Helpers
    {
        public static TransactionScope CreateTransactionScope(int seconds = 60)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public static Guid? StringToGuidDef(string str)
        {
            if (Guid.TryParse(str, out Guid value))
            {
                return value;
            }
            return null;
        }
    }
}
