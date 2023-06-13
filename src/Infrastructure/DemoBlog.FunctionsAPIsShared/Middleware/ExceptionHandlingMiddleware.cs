using System.Net;
using DemoBlog.Domain.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace DemoBlog.FunctionsAPIsShared;

public sealed class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            var httpReqData = await context.GetHttpRequestDataAsync();
            if (httpReqData is null)
                return;

            var invocationResult = context.GetInvocationResult();
            await HandleExceptionAsync(httpReqData, invocationResult, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpRequestData context, InvocationResult invocationResult, Exception e)
    {
        if (e is AggregateException aggregateException)
        {
            if (aggregateException.InnerExceptions.Count > 1)
            {
                await CreateResponse(context, invocationResult, HttpStatusCode.InternalServerError, e.Message);
                return;
            }

            e = aggregateException.InnerExceptions.FirstOrDefault();
        }

        var statusCode = e switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        await CreateResponse(context, invocationResult, statusCode, e.Message);
    }

    private static async Task CreateResponse(
        HttpRequestData context,
        InvocationResult invocationResult,
        HttpStatusCode statusCode,
        string message)
    {
        var response = context.CreateResponse(statusCode);

        // You need to explicitly pass the status code in WriteAsJsonAsync method.
        // https://github.com/Azure/azure-functions-dotnet-worker/issues/776
        await response.WriteAsJsonAsync(new { ErrorMessage = message }, statusCode);

        invocationResult.Value = response;
    }
}
