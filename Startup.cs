using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Models;

namespace QuizApplication
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
            //services.AddDbContext<QuizApplicationContext>(opt =>
            //   opt.UseInMemoryDatabase("QuizApplicationList"));
            //services.AddScoped<>();
            //services.AddEntityFrameworkSqlServer().AddDbContext<QuizApplicationContext>(
            //services.AddDbContext<QuizApplicationContext>(
            //    options => options.UseSqlServer(
            //        //@"Server=DESKTOP-HNPMQTA\CKC;Database=test_db;User Id=test_user;",
            //        @"Server = localhost\MSSQLSERVER01; Database = quiz_db; Trusted_Connection = True;",
            //        providerOptions => providerOptions.EnableRetryOnFailure()
            //    )
            //);

            services.AddDbContext<quiz_dbContext>(options =>
                options.UseSqlServer(@"Server = localhost\MSSQLSERVER01; Database = quiz_db; Trusted_Connection = True;")
            );

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}