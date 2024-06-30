using Narya.Sms.Core.Extensions;

namespace Narya.Sms.Core.Models;

public class SmsOptions
{
    private SmsOptions() { }
    private SmsOptions(ICollection<string> to, string message)
    {
        To = to;
        Message = message;
    }

    public ICollection<string> To { get; private set; } = new List<string>();
    public string Message { get; private set; }

    public static Result<SmsOptions> Create(string message, params string[] to)
    {
        var errors = new List<string>();
        foreach (var number in to)
        {
            if (number.IsValidPhoneNumber() is false)
                errors.Add($"Invalid Phone Number {number}.");
        }
        if(errors.Any()) return Result<SmsOptions>.Failure(errors);
        SmsOptions sms = new SmsOptions(to, message);
        return Result<SmsOptions>.Success(sms);
    }

}