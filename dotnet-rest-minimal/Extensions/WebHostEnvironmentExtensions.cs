namespace JCS.Neon.Rest.Api.Extensions;

public static class WebHostEnvironmentExtensions
{
    /// <summary>
    /// Extension which allows for a check to see if we're running in an integration environment
    /// </summary>
    /// <param name="environment">A <see cref="IWebHostEnvironment"/></param>
    /// <returns></returns>
    public static bool IsIntegration(this IWebHostEnvironment environment)
    {
        return environment.EnvironmentName == "Integration";
    }
}