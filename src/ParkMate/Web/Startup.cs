using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ParkMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ParkMate.Infrastructure.Services;
using Microsoft.AspNetCore.HttpOverrides;
using MongoDB.Driver;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.Web.Util;

namespace ParkMate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:Identity"]));

            services.AddDbContext<ParkMateDbContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:ParkMateDB"], 
                o => o.UseNetTopologySuite()));

            services.Configure<MongoSettings>(options =>
            {
                options.Database = "ParkMateReadDb";
                options.ConnectionString = Configuration["ConnectionStrings:ParkMateReadDB"];
            });
            
            services.AddSingleton<IMongoClient, MongoClient>( 
                _ => new MongoClient(Configuration["ConnectionStrings:ParkMateReadDB"]));
            
            services.AddIdentity<ParkMateUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

           
            services.AddMediatR(typeof(RegisterNewParkingSpaceCommand).Assembly);
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IRepository<ParkingSpace>, ParkingSpaceRepository>();
            services.AddScoped<IDocumentWriteRepository, DocumentRepository>();
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddSingleton<ImageProcessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}