using Microsoft.AspNetCore.Diagnostics;

namespace Wpm.Web.Handlers;

public class WpmExceptionHandler(ILogger<WpmExceptionHandler> logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                          Exception exception,
                                          CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception has occurred while executing the request.");
        httpContext.Response.Redirect("/");
        return ValueTask.FromResult(true);
    }
}
