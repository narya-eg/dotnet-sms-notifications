using Narya.Sms.Core.Models;

namespace Narya.Sms.Core.Interfaces;

public interface ISmsService
{
    Task<Result> Send(SmsOptions options);
    Task<Result> Send(SmsOptions options, dynamic configuration);
}