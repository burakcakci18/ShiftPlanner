using Common.Base.Interfaces.Repository;
using Common.Base.Interfaces.UnitOfWork;
using Common.Infrastructure.Context;
using Common.Infrastructure.Repository;
using Common.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.IoC
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Logging servis
            services.AddSingleton<ILogging, LoggingManager>();

            // Core servisler
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        }
    }
}
