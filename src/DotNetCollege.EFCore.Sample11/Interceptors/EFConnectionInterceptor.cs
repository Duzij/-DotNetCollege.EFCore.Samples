using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample11.Interceptors
{
    public class EFConnectionInterceptor : DbConnectionInterceptor
    {
        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.ConnectionOpened(connection, eventData);
        }

        public override void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.ConnectionClosed(connection, eventData);
        }

        public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.ConnectionFailed(connection, eventData);
        }
    }
}
