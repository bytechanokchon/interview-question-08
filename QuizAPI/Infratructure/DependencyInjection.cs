using Microsoft.Extensions.DependencyInjection;

namespace Infratructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencyInjection(this  IServiceCollection services)
        {
            return services;
        }
    }
}
