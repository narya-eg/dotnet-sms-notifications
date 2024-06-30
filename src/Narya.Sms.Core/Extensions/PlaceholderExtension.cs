using Narya.Sms.Core.Models;

namespace Narya.Sms.Core.Extensions;

internal static class PlaceholderExtension
{
    public static string ReplacePlaceholders(this string text, List<SmsPlaceholder> placeholders)
    {
        foreach (var item in placeholders)
        {
            var oldValue = item.Placeholder.Trim();
            text = text.Replace(oldValue, item.Value);
        }

        return text;
    }
}