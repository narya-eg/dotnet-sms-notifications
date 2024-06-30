namespace Narya.Sms.Core.Interfaces;

public class SmsProvider : ISmsProvider
{
    private readonly IDictionary<string, ISmsService> _providers;

    private SmsProvider()
    {
        _providers = new Dictionary<string, ISmsService>();
    }

    // Public static property to provide access to the single instance
    public static SmsProvider Instance { get; } = new();

    public ISmsService GetProvider(string provider)
    {
        if (_providers.TryGetValue(provider, out var emailService)) return emailService;
        throw new ArgumentException($"Bus with name {provider} not found.");
    }

    public void AddProvider(string provider, ISmsService emailService)
    {
        if (!_providers.ContainsKey(provider)) _providers.Add(provider, emailService);
    }
}