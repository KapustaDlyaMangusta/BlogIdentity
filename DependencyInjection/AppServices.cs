using BlogIdentity.EntityFramework;
using BlogIdentity.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIdentity.DependencyInjection
{
    public static class AppServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("IdentityDatabase")));


            return services;
        }
    }
}
