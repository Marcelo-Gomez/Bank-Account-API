using Domain.Consts;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionHandlerMiddleware
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    IExceptionHandlerFeature? exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        Exception? exception = exceptionHandlerFeature.Error;

                        ProblemDetails? problemDetails = new();

                        if (exception is ValidationException validationException)
                        {
                            problemDetails = ConfigureProblemDetails(context.Request.HttpContext.Request.Path, 
                                ProblemDetailTypeConst.BadRequest, ProblemDetailTitleConst.BadRequestValidation, 
                                StatusCodes.Status400BadRequest, validationException.Message);
                        }
                        else if(exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails = ConfigureProblemDetails(context.Request.HttpContext.Request.Path, 
                                ProblemDetailTypeConst.BadRequest, ProblemDetailTitleConst.BadRequestHttp, 
                                StatusCodes.Status400BadRequest, badHttpRequestException.Message);
                        }
                        else
                        {
                            problemDetails = ConfigureProblemDetails(context.Request.HttpContext.Request.Path, 
                                ProblemDetailTypeConst.InternalServerError, ProblemDetailTitleConst.InternalServerError, 
                                StatusCodes.Status500InternalServerError, ProblemDetailConst.InternalServerError);
                        }

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

                        await context.Response.WriteAsync(JsonSerializer.Serialize<object>(problemDetails));
                    }
                });
            });
        }

        private static ProblemDetails ConfigureProblemDetails(PathString path, string type, string title, int status, string detail)
            => new()
            {
                Instance = path,
                Type = type,
                Title = title,
                Status = status,
                Detail = detail
            };
    }
}