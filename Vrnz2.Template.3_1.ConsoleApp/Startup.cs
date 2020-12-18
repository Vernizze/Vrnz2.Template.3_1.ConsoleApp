using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.BaseContracts.Interfaces;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.Logs;
using Vrnz2.BaseInfra.ServiceCollection;
using Vrnz2.BaseInfra.Settings;
using Vrnz2.BaseInfra.Validations;
using Vrnz2.Template._3_1.ConsoleApp.Settings;

namespace Vrnz2.Template._3_1.ConsoleApp
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
            => new ServiceCollection()
                .AddSettings<AppSettings>()
                .AddLogs()
                .AddAutoMapper(AssembliesHelper.GetAssemblies())
                .AddMediatR(AssembliesHelper.GetAssemblies<ValidationHelper>())
                .AddIServiceColletion()
                .AddBaseValidations()
                .MakeServiceProvider();

        private static ServiceProvider GetServiceProvider { get; set; }

        public static T GetService<T>()
            => GetServiceProvider.GetService<T>();

        private static IServiceCollection MakeServiceProvider(this IServiceCollection services)
        {
            GetServiceProvider = services.BuildServiceProvider();

            return services;
        }

        public static List<Type> GetStartableServices()
        {
            var models = new List<Type>();

            foreach (Type type in AssembliesHelper.GetAssemblies().GetTypes())
            {
                if (type.GetInterfaces().Contains(typeof(IBaseStarter)))
                    models.Add(type);
            }

            return models;
        }
    }
}
