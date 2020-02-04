using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    public static class ServiceCollectionMixins
    {
        public static void RemoveDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (services.FirstOrDefault(x => x.ServiceType == typeof(TContext)) is ServiceDescriptor found)
            {
                services.Remove(found);
            }
            if (services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions)) is ServiceDescriptor dbOptions)
            {
                services.Remove(dbOptions);
            }
            if (services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<TContext>)) is ServiceDescriptor typedOptions)
            {
                services.Remove(typedOptions);
            }
        }
    }
}
