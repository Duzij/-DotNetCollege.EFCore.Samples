using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample11.Interceptors
{
    public class EFCommandInterceptor : DbCommandInterceptor
    {

        public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            Console.WriteLine(eventData.ToString());
            return base.CommandCreated(eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            Console.WriteLine(eventData.ToString());
            return base.ReaderExecuted(command, eventData, result);
        }


    }
}
