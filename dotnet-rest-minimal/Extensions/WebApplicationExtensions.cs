namespace JCS.Neon.Rest.Api.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// This is the top level extension method that configures and maps all the available api routes
    /// </summary>
    /// <param name="application">The constructed <see cref="WebApplication"/> instance</param>
    /// <returns>The <see cref="WebApplication"/> instance</returns>
    public static WebApplication MapCoreApiEndpoints(this WebApplication application)
    {
        return application;
    }
}