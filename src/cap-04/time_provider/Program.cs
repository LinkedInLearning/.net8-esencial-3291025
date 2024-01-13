var madrid = new MyTimeProvider(TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"));
var mexico = new MyTimeProvider(TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));


System.Console.WriteLine(madrid.GetLocalNow());
System.Console.WriteLine(mexico.GetLocalNow());
System.Console.WriteLine(DateTimeOffset.Now);
class MyTimeProvider(TimeZoneInfo tzi) : TimeProvider
{
    public override TimeZoneInfo LocalTimeZone => tzi;

}