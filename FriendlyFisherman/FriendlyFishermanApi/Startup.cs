using Administration.DataAccess;
using FriendlyFisherman.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Publishing.DataAccess;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Impl;
using Newtonsoft.Json;
using Publishing.Domain.Repositories.Categories;
using Publishing.DataAccess.Repositories.Categories;
using Publishing.DataAccess.Repositories.Threads;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Categories;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Implementation.Categories;
using Publishing.Services.Implementation.Threads;
using Users.DataAccess;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Domain.Repositories;
using Users.Services.Abstraction;
using Users.Services.Implementation;

namespace FriendlyFishermanApi
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

            #region Bearer Setup
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Friendly Fisherman API",
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
            #endregion
            #region Contexts
            #region Administration
            services.AddDbContext<AdministrationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            #region Publishing
            services.AddDbContext<PublishingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            #region User
            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<UsersDbContext>();
            #endregion
            #endregion
            #region DI
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonalMessagesRepository, PersonalMessagesRepository>();
            services.AddScoped<IThreadCategoriesRepository, ThreadCategoriesRepository>();
            services.AddScoped<IThreadsRepository, ThreadsRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonalMessagesService, PersonalMessagesService>();
            services.AddScoped<IThreadCategoriesService, ThreadCategoriesService>();
            services.AddScoped<IThreadsService, ThreadsService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton(Configuration.GetSection(typeof(AppSettings).Name).Get<AppSettings>());
            #endregion

            services.AddCors();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseCors(options =>
                options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
           );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
