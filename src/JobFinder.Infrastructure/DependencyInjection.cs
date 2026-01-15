using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Interfaces;
using JobFinder.Infrastructure.Persistence.Data;
using JobFinder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobFinder.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
            });

            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
