using System.Transactions;

namespace TaskManagerTests.Service
{
    internal static class ScopeFactory
    {
        /// <summary> 
        /// returns a transaction to work with the DB, change the time to a longer one for debug mode
        /// </summary>
        public static TransactionScope GetScope(int seconds = 1)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
