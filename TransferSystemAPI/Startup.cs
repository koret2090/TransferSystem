using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using ComponentAccessToDB.RepositoryInterfaces;
using ComponentAccessToDB.RepositoryImplementation;
using Controllers;
using Models.ModelsDB;

namespace TransferSystemAPI
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
            services.AddControllers();
            
            AddRepositoryExtensions(services);
            AddControllerExtensions(services);
            AddLoggerExtensions(services);
            
            // Because of exception "possible object cycle was detected which is not supported"
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            var user = new Userinfo()
            {
                Id = 101,
                Login = "admin",
                Hash = "admin",
                Permission = 3
            };

            services.AddDbContext<transfersystemContext>(option => option.UseNpgsql(Configuration["Connections:CurrentConnection"]));
            services.AddSingleton(provider => { return user; });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TransferSystemAPI", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransferSystemAPI v1"));
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddLoggerExtensions(IServiceCollection services)
        {
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.File(Configuration["Logger"])
                .CreateLogger();
            
            services.AddLogging(x =>
            {
                x.AddSerilog(logger: serilogLogger, dispose: true);
            });
        }
        
        private void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IPlayerStatisticsRepository, PlayerStatisticsRepository>();
            services.AddScoped<IAvailableDealsRepository, AvailableDealsRepository>();
            services.AddScoped<IDesiredPlayersRepository, DesiredPlayersRepository>();
            services.AddScoped<IManagementRepository, ManagementRepository>();
            services.AddScoped<IFunctionsRepository, FunctionRepository>();
            services.AddScoped<IPlayerSpecificationsRepository, PlayerSpecificationsRepository>();
            services.AddScoped<ITeamStatisticsRepository, TeamStatisticsRepository>();
        }
        private void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<AvailableDealsController>();
            services.AddScoped<DesiredPlayersController>();
            services.AddScoped<FunctionController>();
            services.AddScoped<ManagementController>();
            services.AddScoped<PlayerController>();
            services.AddScoped<PlayerStatisticsController>();
            services.AddScoped<PlayerSpecificationsController>();
            services.AddScoped<TeamController>();
            services.AddScoped<TeamStatisticsController>();
            services.AddScoped<UserInfoController>();
        }
    }
}