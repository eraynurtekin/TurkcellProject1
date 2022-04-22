using FastShop.Business;
using FastShop.Business.MapperProfile;
using FastShop.DataAccess.Data;
using FastShop.DataAccess.Data.MongoConnections;
using FastShop.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastShop.Web
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

            //services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            //services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            //serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            
            services.AddControllersWithViews();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository,EfCategoryRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository,EfUserRepository>();

            var connectionString = Configuration.GetConnectionString("SqlDb");
            services.AddDbContext<FastShopDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(MapProfile));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opt =>
                    {
                        opt.LoginPath = "/Login/Index";
                        opt.AccessDeniedPath = "/Login/AccessDenied";
                    });


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

                endpoints.MapControllerRoute(
                   name: "xxxx",
                   pattern: "Kategori{catid}",
                   defaults: new { controller = "Home", action = "Index",catId=1 });


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
