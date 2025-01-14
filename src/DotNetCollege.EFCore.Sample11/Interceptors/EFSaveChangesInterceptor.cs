﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample11.Interceptors
{
    public class EFSaveChangesInterceptor : SaveChangesInterceptor
    {
        public EFSaveChangesInterceptor()
        {
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            Console.WriteLine(eventData.ToString());
            return base.SavedChanges(eventData, result);
        }

        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            Console.WriteLine(eventData.ToString());
            base.SaveChangesFailed(eventData);
        }
    }
}
