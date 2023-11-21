using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApi.Handlers;

public class ApplicationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ApplicationException error)
        {
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Error = new
                {
                    Code = ReasonPhrases.GetReasonPhrase(httpContext.Response.StatusCode).ToLowerInvariant().Replace(" ", "_"),
                    httpContext.Response.StatusCode,
                    error.Message
                }
            },
            cancellationToken: cancellationToken);

            return true;
        }

        return false;
    }
}
