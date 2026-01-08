using Expenses.API.Data.Services.Authentication;
using System.Security.Claims;

namespace Expenses.API.Data.Services.CurrentUser
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?
                    .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    throw new UnauthorizedAccessException("User is not authenticated");

                return int.Parse(userIdClaim);
            }
        }
    }
}
