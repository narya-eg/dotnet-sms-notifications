using Microsoft.Extensions.DependencyInjection;
using Narya.Sms.Core.Interfaces;

namespace Narya.Sms.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddSmsProvider(this IServiceCollection services,
        Func<IServiceCollection, IServiceCollection> register)
    {
        services = register(services);
        services.AddSingleton<ISmsProvider>(provider => SmsProvider.Instance);
        return services;
    }

    public static IServiceProvider AddSmsProvider(this IServiceProvider serviceProvider, string provider,
        ISmsService providerService)
    {
        var emailProvider = SmsProvider.Instance;
        emailProvider.AddProvider(provider, providerService);
        return serviceProvider;
    }
}