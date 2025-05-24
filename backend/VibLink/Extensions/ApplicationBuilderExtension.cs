namespace VibLink.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseViblinkMiddleware(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
