using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using MyEShop.Middlewares;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace MyEShop.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly IRazorViewEngine viewEngine;
        private readonly ITempDataProvider tempDataProvider;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            this.next = next;
            this.logger = logger;
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");

                // Clear the response to start rendering the error view.
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Get the area name from the route data (if available).
                var areaName = context.GetRouteValue("area")?.ToString();

                // Specify the view name based on the area.
                var viewName = string.IsNullOrEmpty(areaName) ? "ServerError" : $"{areaName}Error";

                // Render the view and write it to the response.
                await RenderViewAsync(context, viewName);
            }

            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                // Clear the response to start rendering the 404 view.
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                // Render the view and write it to the response.
                var areaName = context.GetRouteValue("area")?.ToString();

                // Specify the view name based on the area.
                var viewName = string.IsNullOrEmpty(areaName) ? "NotFound" : $"{areaName}NotFound";

                // Render the view and write it to the response.
                await RenderViewAsync(context, viewName);
            }
        }

        private async Task RenderViewAsync(HttpContext context, string viewName)
        {
            var actionContext = new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor());

            using var sw = new StringWriter();
            var viewResult = viewEngine.FindView(actionContext, viewName, false);

            if (!viewResult.Success)
            {
                // Fallback to a default error view if the specific area error view is not found.
                viewResult = viewEngine.FindView(actionContext, "Error/ServerError", false);

                if (!viewResult.Success)
                {
                    throw new InvalidOperationException($"View '{viewName}' not found");
                }
            }

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
                new TempDataDictionary(context, tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(sw.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
