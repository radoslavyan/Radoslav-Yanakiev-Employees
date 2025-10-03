using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PairOfEmployees.Application.Contracts;
using PairOfEmployees.Domain.Models;
using PairOfEmployees.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddDbContext<PairDbContext>(opt => opt.UseInMemoryDatabase("PairOfEmployeesDB"));

            services.AddScoped<IPairOfEmployeesLoader, Loader.CsvLoader>();
            services.AddScoped<IPairOfEmployeesRepository, Repositories.PairOfEmployeesRepository>();

            return services;
        }
    }

    
}
