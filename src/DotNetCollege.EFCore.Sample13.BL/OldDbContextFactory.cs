using DotNetCollege.EFCore.Sample13.DAL;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample13.BL.Services
{
    public class OldDbContextFactory : IOldDbContextFactory, IDisposable
    {
            private readonly Lazy<AppDbContext> databaseContext;

            public OldDbContextFactory(IServiceProvider serviceProvider)
            {
                Debug.WriteLine("Created factory");
                databaseContext = new Lazy<AppDbContext>(serviceProvider.GetRequiredService<AppDbContext>);
            }

            public AppDbContext GetContext()
            {
                return databaseContext.Value;
            }

            public void Dispose()
            {
                if (databaseContext.IsValueCreated)
                {
                    Debug.WriteLine("Dispose of context");
                    databaseContext.Value.Dispose();
                }
                Debug.WriteLine("Dispose of factory");
            }
    }
}
