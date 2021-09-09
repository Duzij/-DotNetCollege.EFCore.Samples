using DotNetCollege.EFCore.Sample13.BL.Services;
using DotNetCollege.EFCore.Sample13.DAL;
using DotNetCollege.EFCore.Web.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample13
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CustomDbContextOptions>(Configuration.GetSection("DbOptions"));

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));


            //needed to add both for Identity services
            services.AddTransient(p => p.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

            services.AddDbContextFactory<AppDbContext>(options => {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddHttpContextAccessor();

            services.AddScoped<CategoryFacade>();
            services.AddScoped<ProductRepository>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            //using (app.ApplicationServices.CreateScope())
            //{
            //    var context = app.ApplicationServices.GetRequiredService<IDbContextFactory<AppDbContext>>();
            //    using (var db = context.CreateDbContext())
            //    {
            //        db.Database.EnsureDeleted();
            //        db.Database.EnsureCreated();
            //    }
            //}

        }
    }
}
