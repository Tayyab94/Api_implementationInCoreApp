using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_GUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Book_GUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICountryRepositoryGUI, CountryRepositoryGUI>();
            services.AddScoped<IAuthorRepositoryGUI, AuthorRepositoryGUI>();
            services.AddScoped<ICategoryRepositoryGUI, CategoryRepostoryGUI>();
            services.AddScoped<IReviewerRepositoryGUI, ReviewerRepositoryGUI>();
            services.AddScoped<IReviewRepositoryGUI, ReviewRepositoryGUI>();
            services.AddScoped<IBookRepositoryGUI, BookRepositoryGUI>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

                 app.UseStaticFiles();                  // (1) 
                 app.UseStatusCodePages();              //(2)
                 app.UseMvc(routes=> {
                     routes.MapRoute(
                         name: "default",
                         template: "{controller=Home}/{action=Index}/{id?}"
                         );
                 });                          //(3) Order Matters Alot
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
