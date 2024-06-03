namespace Alerts.WPF.Hepler;

public static class TimeSpanHelper
{
    public static string TimeSpanConvertString(this TimeSpan timeSpan)
    {
        return timeSpan switch
        {
            { Days: > 0 } => $"{timeSpan.Days} дн. {timeSpan.Hours} год. {timeSpan.Minutes} хв",
            { Hours: > 0 } => $"{timeSpan.Hours} год. {timeSpan.Minutes} хв",
            _ => $"{timeSpan.Minutes} хв"
        };
    }
}