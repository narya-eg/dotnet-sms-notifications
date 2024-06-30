namespace Narya.Sms.Core.Models;

public sealed class SmsPlaceholder
{
    private SmsPlaceholder()
    {
    }

    public SmsPlaceholder(string placeholder, string value)
    {
        Placeholder = placeholder;
        Value = value;
    }

    public string Placeholder { get; private set; }
    public string Value { get; private set; }
}