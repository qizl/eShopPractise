using EnjoyCodes.eShopOnContainers.Services.BasketAPI.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Infrastructure.Exceptions
{
    public static class FailingMiddlewareAppBuilderExtensions
    {
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder) => UseFailingMiddleware(builder, null);
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder, Action<FailingOptions> action)
        {
            var options = new FailingOptions();
            action?.Invoke(options);
            builder.UseMiddleware<FailingMiddleware>(options);
            return builder;
        }
    }
}
