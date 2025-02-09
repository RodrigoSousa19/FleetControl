using FleetControl.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.ExceptionsHandler
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is BusinessException businessException)
            {
                var errors = businessException.Errors.Select(e => new { Field = e.Field, Message = e.Message });

                var problemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Erro de validação",
                    Detail = "Um ou mais erros de validação ocorreram.",
                };

                foreach (var error in errors)
                {
                    problemDetails.Errors.Add(error.Field, new[] { error.Message });
                }

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }
            else
            {
                var details = new ProblemDetails
                {
                    Detail = exception.StackTrace,
                    Title = "Erro inesperado."
                };

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
            }

            return true;
        }
    }
}
