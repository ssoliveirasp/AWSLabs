using System.Reflection;

namespace lambdaApi.Domain;
public class OnlineService
{
    public static string Works => $"works Domain " + 
                                  $"Version 1.1: {Assembly.GetExecutingAssembly().GetName().Version} " +
                                  $"Date:{DateTime.UtcNow}";
}