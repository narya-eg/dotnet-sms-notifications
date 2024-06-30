using Microsoft.Extensions.DependencyInjection;
using Narya.Sms.Core.Interfaces;
using Narya.Sms.Core;
using Narya.Sms.Twilio.Services;

namespace Narya.Sms.Twilio;

public static class DependencyInjection
{
    public static IServiceCollection AddTwilioProvider(this IServiceCollection services)
    {
        services.AddSingleton<ISmsService, SmsService>();
        services.AddSingleton<SmsService>();
        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.AddSmsProvider("Twilio", serviceProvider.GetRequiredService<SmsService>());
        return services;
    }
}