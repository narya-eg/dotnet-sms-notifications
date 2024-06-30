using Microsoft.Extensions.Configuration;
using Narya.Sms.Core.Models;
using Narya.Sms.Twilio.Extensions;
using Narya.Sms.Core.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Narya.Sms.Core.Extensions;

namespace Narya.Sms.Twilio.Services;

public class SmsService : ISmsService
{
    private readonly IConfiguration _configuration;
    private TwilioConfig _twilioConfig = new();

    public SmsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Result> Send(SmsOptions options)
    {
        var result = _configuration.GetTwilioConfig();
        if (result.IsFailure) return Result.Failure(result.Errors);
        _twilioConfig = result.Value;
        await SendSms(options);
        return Result.Success();
    }

    public async Task<Result> Send(SmsOptions options, dynamic configuration)
    {
        if (configuration is not object) return Result.Failure("Twilio configuration is not a valid configurations.");
        Result<TwilioConfig> result = ModelExtension.ConvertTo<TwilioConfig>(configuration);
        if (result.IsFailure) return Result.Failure(result.Errors);
        _twilioConfig = result.Value;
        await SendSms(options);
        return Result.Success();
    }

    private async Task SendSms(SmsOptions options)
    {
        TwilioClient.Init(_twilioConfig.AccountSID, _twilioConfig.AuthToken);

        var messageResource = await MessageResource.CreateAsync(
            new PhoneNumber(string.Join(",", options.To)),
            from: new PhoneNumber(_twilioConfig.From),
            body: options.Message);
    }
}