using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using JobFinder.Application.Behaviors;

namespace JobFinder.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IHostApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<VacancyProfile>();
                cfg.AddProfile<ProfileProfile>();
            });
        }
    }
}
