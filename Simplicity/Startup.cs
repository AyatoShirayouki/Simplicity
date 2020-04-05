using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Simplicity.Entities;
using Simplicity.GlobalExceptionHandling;
using Simplicity.Helpers;
using Simplicity.Mappings;
using Simplicity.Repositories.Repositories;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.Services;
using Simplicity.Services.ServicesInterfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Simplicity
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
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials();
            }));


            services.AddDbContext<SimplicityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SimplicityDbConnection"));
            });

            services.AddAutoMapper(m => m.AddProfile(new AutomapperMappings()));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Simplicity API", Version = "v1" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "access token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
                
            });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUsersService>();
                        var userId = context.Principal.Identity.Name;
                        var user = userService.GetAll().FirstOrDefault(u=>u.Username == userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                };
            });
            services.AddSignalR();

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                config.AddPolicy("Moderator", policy => policy.RequireRole("Moderator"));
                config.AddPolicy("AdminMod", policy => policy.RequireAssertion(
                    context => context.User.HasClaim(x=>x.Type == ClaimTypes.Role && 
                        (x.Value == "Administrator" ||
                        x.Value == "Moderator"))
                ));
                config.AddPolicy("User", policy => policy.RequireRole("User"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IUsersService), typeof(UsersService));
            services.AddScoped(typeof(IProjectsService), typeof(ProjectsService));
            services.AddScoped(typeof(IUsersRepository), typeof(UsersRepository));
            services.AddScoped(typeof(IProjectsRepository), typeof(ProjectsRepository));
            services.AddScoped(typeof(IUsersProjectsService), typeof(UsersProjectsService));
            services.AddScoped(typeof(IUsersProjectsRepository), typeof(UsersProjectsRepository));
            services.AddScoped(typeof(ITicketsService), typeof(TicketsService));
            services.AddScoped(typeof(ITicketsRepository), typeof(TicketsRepository));
            services.AddSingleton(Configuration.GetSection(typeof(AppSettings).Name).Get<AppSettings>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionHandler();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
               routes.MapHub<MessageHub>("/location")
           );
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simplicty API");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseMvc();
        }
    }
}
