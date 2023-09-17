using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Services;
using FitnessPortalAPI.Services.Interfaces;

namespace FitnessPortalAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ITokenStore _tokenStore;
        public ErrorHandlingMiddleware(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                //var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var authorizationHeader = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    var token = authorizationHeader.Replace("Bearer ", "");
                    var isTokenBlacklisted = await _tokenStore.IsTokenBlacklistedAsync(token);
                    if (isTokenBlacklisted)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                }

                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
