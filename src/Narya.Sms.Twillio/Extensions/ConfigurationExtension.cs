using Microsoft.Extensions.Configuration;
using Narya.Sms.Core.Interfaces;
using Narya.Sms.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Narya.Sms.Twilio.Extensions;

public static class ConfigurationExtension
{
    public static Result<TwilioConfig> GetTwilioConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Twilio").Get<TwilioConfig>();
        if (config is null) return Result<TwilioConfig>.Failure("Missing 'Twilio' configuration section from the appsettings.");
        if (config.ValidateObject(config, out List<ValidationResult> results) is false) return Result<TwilioConfig>.Failure(results.Select(x => x.ErrorMessage ?? "").ToList());
        return Result<TwilioConfig>.Success(config);
    }
}

public sealed class TwilioConfig : IProviderConfig
{
    [Required] public string? AccountSID { get; set; }
    [Required] public string? AuthToken { get; set; }
    [Required] public string? PathServiceSid { get; set; }
    [Required] public string? From { get; set; }

    public bool ValidateProperty(object instance, string propertyName, object? value)
    {
        return Validator.TryValidateProperty(value, new ValidationContext(instance) { MemberName = propertyName },
            new List<ValidationResult>());
    }

    public bool ValidateObject(object instance, out List<ValidationResult> validationResults)
    {
        validationResults = new();
        return Validator.TryValidateObject(instance, new ValidationContext(instance),
            validationResults);
    }
}