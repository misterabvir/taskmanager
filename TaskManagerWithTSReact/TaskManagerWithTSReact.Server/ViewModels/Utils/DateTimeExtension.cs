namespace TaskManagerWithTSReact.Server.ViewModels.Utils;

public static class DateTimeExtension
{
    private const string ERROR = "wrong time input";
    private const string FORMAT = "MM/dd/yyyy h:mm tt";
    private const string EMPTYFORMAT = "-";
    private const string DAYS = "day(s)";
    private const string HOURS = "hour(s)";
    private const string MINUTES = "minute(s)";
    
    private static string GetString(this double value, string postfix)
    {
        return $"{(int)value} {postfix}";
    }

    public static string HowLong(this DateTime? start, DateTime? end)
    {
        string result = EMPTYFORMAT;
        if(start == null) return result;
        TimeSpan? time;

        if (end != null) time = end - start;
        else time = DateTime.Now - start;

        if (time == null) throw new Exception(ERROR);

        if (time.Value.TotalDays > 1) result = time.Value.TotalDays.GetString(DAYS);
        else if (time.Value.TotalHours > 1) result = time.Value.TotalHours.GetString(HOURS);
        else result = time.Value.TotalMinutes.GetString(MINUTES);

        return result;
    }

    public static string GetFormatString(this DateTime? dateTime)
    {
        if (dateTime == null) return EMPTYFORMAT;
        return dateTime.Value.GetFormatString();
    }
    public static string GetFormatString(this DateTime dateTime)
    {
        return dateTime.ToString(FORMAT);
    }
}
