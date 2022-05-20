using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using middleware;

namespace ExtensionMethods
{
    public static class UserKeyValidatorsExtension
    {
            public static IApplicationBuilder ApplyUserKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserKeyValidatorMiddleware>(); // we will not expose class name
            return app;
        }
    }
}