using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Repositories;

namespace middleware
{
    public class UserKeyValidatorMiddleware // custom middleware
    {
        public readonly RequestDelegate _next;
     

        public UserKeyValidatorMiddleware(RequestDelegate next) // should not be injected.
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,IContactsRepository contactRepository)
          //On every request this func will be called
          //main point of middlware is we have to the interface name in invoke function parameter.
        {
            if (!context.Request.Headers.Keys.Contains("user-key"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }
            else
            {
                if (!contactRepository.CheckValidUserKey(context.Request.Headers["user-key"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}