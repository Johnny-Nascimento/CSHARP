using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Adopet.Exceptions.Handlers
{
    public class NullReferenceExceptionHandle : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NullReferenceException)
                return ValueTask.FromResult(false);

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "Falha ao encontrar objeto solicitado!",
                Status = StatusCodes.Status404NotFound,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.WriteAsJsonAsync(problemDetails);

            return ValueTask.FromResult(true);
        }
    }

    public class PetIndisponivelExceptionHandle : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not PetIndisponivelException)
                return ValueTask.FromResult(false);

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "Pet já foi adotado!",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.WriteAsJsonAsync(problemDetails);

            return ValueTask.FromResult(true);
        }
    }

    public class PetEstaSendoAdotadoExceptionHandle : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not PetEstaSendoAdotadoException)
                return ValueTask.FromResult(false);

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "Pet está em processo de adoção!",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.WriteAsJsonAsync(problemDetails);

            return ValueTask.FromResult(true);
        }
    }

    public class TutorComLimiteAtingidoExceptionHandle : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not TutorComLimiteAtingidoException)
                return ValueTask.FromResult(false);

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "Pet está em processo de adoção!",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.WriteAsJsonAsync(problemDetails);

            return ValueTask.FromResult(true);
        }
    }
}
