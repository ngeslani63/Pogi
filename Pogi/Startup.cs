using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pogi.Data;
using Pogi.Models;
using Pogi.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

namespace Pogi
{
    public class Startup
    {
        private string apiKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //var builder = new ConfigurationBuilder();
            //if (env.IsDevelopment())
            //    builder.AddUserSecrets<AuthMessageSenderOptions>();
            //builder.AddEnvironmentVariables();
            //var dom = builder.Build();
            //Configuration = dom;

        }

        public IConfiguration Configuration { get; }
        public object RouteParameter { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICourseData, SqlCourseData>();
            services.AddScoped<ICourseDetail, SqlCourseDetail>();
            services.AddScoped<ITeeTimeInfo, SqlTeeTimeInfo>();
            services.AddScoped<IPlayerInfo, SqlPlayerInfo>();
            services.AddScoped<ITeeAssignInfo, SqlTeeAssignInfo>();
            services.AddScoped<IMemberData, SqlMemberData>();
            services.AddScoped<IScoreInfo, SqlScoreInfo>();
            services.AddScoped<IHandicap, SqlHandicap>();
            services.AddScoped<ITourInfo, SqlTourInfo>();
            services.AddScoped<IActivity, SqlActivity>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PogiIdentityDb")));

            services.AddDbContext<PogiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PogiDb")));

            services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();
            services.BuildServiceProvider().GetService<PogiDbContext>().Database.Migrate();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //services.AddDataProtection().PersistKeysToFileSystem(GetKeyRingDirInfo()).SetApplicationName("SharedCookieApp");
            //services.ConfigureApplicationCookie(options => {
            //    options.Cookie.Name = ".AspNet.SharedCookie";
            //});

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;

            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });


            //services.Configure<SignInOptions>(signin =>
            //{
            //    // SignIn settings
            //    signin.RequireConfirmedEmail = true;
            //});


            // Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.Configure<AuthMessageSenderOptions>(Configuration);

            apiKey = Configuration.GetSection("SENDGRID_API_KEY").Value;

            services.AddMvc();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            services.AddSession();

        }

        private DirectoryInfo GetKeyRingDirInfo()
        {
            var startupAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var applicationBasePath = System.AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                var keyRingDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, "KeyRing"));
                if (keyRingDirectoryInfo.Exists)
                {
                    return keyRingDirectoryInfo;
                }
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"KeyRing folder could not be located using the application root {applicationBasePath}.");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }
            if (env.IsProduction())
                optionsAccessor.Value.SendGridKey = apiKey;

            var options = new RewriteOptions().AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
