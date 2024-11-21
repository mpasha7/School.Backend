using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SchoolConnect");
            services.AddDbContext<SchoolDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            });
            services.AddScoped<ISchoolDbContext>(provider => provider.GetService<SchoolDbContext>()); // GetService<SchoolDbContext>()
            return services;
        }

    }
}
