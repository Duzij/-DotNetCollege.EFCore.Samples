using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample11.Interceptors
{
    
    public class EFTransactionInterceptor : DbTransactionInterceptor
    {
        public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.TransactionCommitted(transaction, eventData);
        }

        public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.TransactionFailed(transaction, eventData);
        }

    }
}
