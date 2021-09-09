using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Web.DAL
{
    public class EFSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ILogger<EFSaveChangesInterceptor> logger;

        public EFSaveChangesInterceptor(ILogger<EFSaveChangesInterceptor> logger)
        {
            this.logger = logger;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            logger.LogInformation(eventData.EventIdCode);
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
