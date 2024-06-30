namespace Narya.Sms.Core.Interfaces;

public interface ISmsProvider
{
    ISmsService GetProvider(string provider);
    void AddProvider(string provider, ISmsService smsService);
}