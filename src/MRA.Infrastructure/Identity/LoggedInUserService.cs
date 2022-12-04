using Microsoft.AspNetCore.Http;
using MRA.CrossCuttingConcerns.Identity;
using System.Security.Claims;

namespace MRA.Infrastructure.Identity
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _context;
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
            //UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }
        public bool IsAuthenticated
        {
            get
            {
                return _context.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public string UserId
        {
            get
            {
                var userId = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? _context.HttpContext.User.FindFirst("sub")?.Value;

                return userId;
            }
        }

        public List<KeyValuePair<string, string>> Claims
        {
            get
            {
                return _context.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(
                    item.Type,
                    item.Value)).ToList();
            }
        }
    }
}
