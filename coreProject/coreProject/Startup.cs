using coreProject.Contexts;
using coreProject.Entities;
using coreProject.Interfaces;
using coreProject.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        //Ýlgili servis bu kýsýmda eklenir
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<YoutubeContext>();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }
            ).AddEntityFrameworkStores<YoutubeContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Login");
                opt.Cookie.Name = "coreProject";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<ICategoriesRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoriesRepository, ProductCategoryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //Ýlgili servis bu kýsýmda kullanýlýr
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            //admin login
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");
            IdentityInitializer.CreateAdmin(userManager, roleManager);
            app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseSession();


            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute
                (name: "areas", pattern: "{area}/{controller=Product}/{action=Index}/{id?}");

                endpoints.MapControllerRoute
                (name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");

            });
        }
    }
}
