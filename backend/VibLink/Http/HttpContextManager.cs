using System.Security.Claims;

namespace VibLink.Http
{
    public class HttpContextManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext? GetCurrentContext()
        {
            return _httpContextAccessor.HttpContext;
        }

        public ClaimsPrincipal? GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User;
        }

        public bool IsUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string? GetHeader(string key)
        {
            return _httpContextAccessor.HttpContext?.Request.Headers[key] ?? string.Empty;
        }
    }
}
