using AutoMapper;
using ElectronicShop.Application.Authentications.Services;
using ElectronicShop.Application.Common.Mapper;
using ElectronicShop.Application.Products.Services;
using ElectronicShop.Data.EF;
using ElectronicShop.Data.Entities;
using ElectronicShop.WebApi.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ElectronicShop.WebApi
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
            // Model state validation filter ASP.NET Core
            services.AddScoped<ValidationFilterAttribute>();

            // Handler for MediatR query ASP.Net Core
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ElectronicShopDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultDb")));

            // Fix .Net Core 3.1 possible object cycle was detected which is not supported
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Compatibility version for ASP.NET Core MVC
            services.AddMvc(); //.SetCompatibilityVersion(CompatibilityVersion.Latest);

            // For use Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // For use AutoMapper
            services.AddAutoMapper(typeof(ElectronicShopProfile));

            // For use ElectronicShopDbContext
            services.AddDbContext<ElectronicShopDbContext>();
            services.AddIdentity<AspNetUser, AspNetRole>()
                .AddEntityFrameworkStores<ElectronicShopDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            //Dependency Injection
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<UserManager<AspNetUser>, UserManager<AspNetUser>>();
            services.AddTransient<SignInManager<AspNetUser>, SignInManager<AspNetUser>>();

            services.AddTransient<IProductService, ProductService>();

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElectronicShop API V1"); });

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
