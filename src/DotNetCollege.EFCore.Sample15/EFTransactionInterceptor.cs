using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample15.Interceptors
{
    public class EFTransactionInterceptor : DbTransactionInterceptor
    {
        public override InterceptionResult<DbTransaction> TransactionStarting(DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result)
        {
            WriteToConsole(eventData);
            return base.TransactionStarting(connection, eventData, result);
        }

        public override DbTransaction TransactionStarted(DbConnection connection, TransactionEndEventData eventData, DbTransaction result)
        {
            WriteToConsole(eventData);
            return base.TransactionStarted(connection, eventData, result);
        }


        public override DbTransaction TransactionUsed(DbConnection connection, TransactionEventData eventData, DbTransaction result)
        {
            WriteToConsole(eventData);
            return base.TransactionUsed(connection, eventData, result);
        }

        public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            WriteToConsole(eventData);
            base.TransactionCommitted(transaction, eventData);
        }

        public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            WriteToConsole(eventData);
            base.TransactionFailed(transaction, eventData);
        }

        public override void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
        {
            WriteToConsole(eventData);
            base.TransactionRolledBack(transaction, eventData);
        }


        public override void CreatedSavepoint(DbTransaction transaction, TransactionEventData eventData)
        {
            WriteToConsole(eventData);
            base.CreatedSavepoint(transaction, eventData);
        }
        public override void ReleasedSavepoint(DbTransaction transaction, TransactionEventData eventData)
        {
            WriteToConsole(eventData);
            base.ReleasedSavepoint(transaction, eventData);
        }

        public override void RolledBackToSavepoint(DbTransaction transaction, TransactionEventData eventData)
        {
            WriteToConsole(eventData);
            base.RolledBackToSavepoint(transaction, eventData);
        }

        private static void WriteToConsole(EventData eventData)
        {
            Console.WriteLine(new string('-', 10));
            Console.WriteLine(eventData.ToString());
            Console.WriteLine(new string('-', 10));
        }
    }
}
