using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ComponentAccessToDB.RepositoryInterfaces;
using ComponentAccessToDB.RepositoryImplementation;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDB;

namespace TechnologicalUI
{
    class Program
    {
        private static string ConnectionString =
            "Host=localhost;Port=5432;Database=transfersystem;Username=postgres;Password=1721";
        
        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Moderator>();
                    AddRepositoryExtensions(services);
                    AddControllerExtensions(services);
                    services.AddDbContext<transfersystemContext>(option => option.UseNpgsql(ConnectionString));

                    var serilogLogger = new LoggerConfiguration()
                        .WriteTo.File("logfile.txt")
                        .CreateLogger();
                    services.AddLogging(x =>
                    {
                        x.AddSerilog(logger: serilogLogger, dispose: true);
                    });
                });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var moderator = services.GetRequiredService<Moderator>();
                moderator.Menu();
            }
        }
        
        private static void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IAvailableDealsRepository, AvailableDealsRepository>();
        }
        private static void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<AvailableDealsController>();
            services.AddScoped<PlayerController>();
            services.AddScoped<TeamController>();
            services.AddScoped<UserInfoController>();
        }
    }
}

