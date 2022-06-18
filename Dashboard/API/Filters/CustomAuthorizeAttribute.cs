using Application.Models;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IDistributedCache _distributedCache;

        public CustomAuthorizeAttribute(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null)
            {
                //token string is from index 7 to the end
                string tokenString = authHeader[7..];

                string isRevokedString = _distributedCache.GetString(tokenString);
                if (isRevokedString != null && Convert.ToBoolean(isRevokedString))
                {
                    context.Result = new ObjectResult(
                        ApiResult<string>.Failure(new List<string>() { Message.GetMessage(ValidatorMessage.Unauthorized) }));

                    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
        }
    }
}