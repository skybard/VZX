using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using VZX.MvcCoreUI.Business.Abstract;
using VZX.MvcCoreUI.Business.Concrete;
using VZX.MvcCoreUI.DataAccess.Abstract;
using VZX.MvcCoreUI.DataAccess.Concrete;
using VZX.MvcCoreUI.DataAccess.Concrete.EntityFramework;
using VZX.MvcCoreUI.DataAccess.Concrete.FakeRepository;

namespace VZX.MvcCoreUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IFileProvider>(
                            new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            //1. Yöntem  appsettings
            //services.AddDbContext<VZXDbContext>(options => options.UseSqlServer(Configuration["dbConnection"]));

            //2. Yöntem  appsettings
            services.AddDbContext<VZXDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IProductServices, ProductManager>();
            //services.AddScoped<IProductDal, EfProductDal>(); //DB üzerinden veri gelmektedir.
            services.AddScoped<IProductDal, FakeProductDal>(); //Static veri gelmektedir.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "productDefault",
                    template: "urunler/urun-ekle",
                    defaults: new { Controller = "Product", Action = "Create" }
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
