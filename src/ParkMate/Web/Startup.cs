using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.HttpOverrides;
using SixLabors.ImageSharp.Web.DependencyInjection;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using ParkMate.Infrastructure.Identity;
using ParkMate.Infrastructure.Services;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.Web.Util;
using ParkMate.Web.Services;

using ApplicationServices.Config;

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

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ParkingSpaceMappingProfile());
                mc.AddProfile(new BookingMappingProfile());
                mc.AddProfile(new CustomerMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

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
            
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Identity/Account/Login"; });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddImageSharp();

            services.AddMediatR(typeof(RegisterNewParkingSpaceCommand).Assembly);
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddScoped<ImageProcessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ParkMateDbContext context)
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
            AddressDataLoader.LoadAddressData(env, context);
        }
    }
}