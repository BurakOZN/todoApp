using Microsoft.Extensions.DependencyInjection;
using MssqlRepository.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MssqlRepository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IParameter), typeof(Parameter));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IJobRepository), typeof(JobRepository));
        }
    }
}
