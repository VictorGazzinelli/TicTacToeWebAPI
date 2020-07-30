using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace TicTacToeWebAPI.Utils.Transaction
{
    public class TransactionHandler
    {
        public static T HandleTransaction<T>(Func<T> function)
        {
            TimeSpan scopeTimeout = TimeSpan.FromMinutes(1);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, scopeTimeout))
            {
                //ConnectionFactory.Instance.Begin();
                try
                {
                    T result = function();
                    scope.Complete();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("TransactionHandler failed to complete the transaction", ex);
                }
                finally
                {
                    //ConnectionFactory.Instance.End();
                    //ConnectionFactory.Instance.Dispose();
                }
            }
        }
    }
}
