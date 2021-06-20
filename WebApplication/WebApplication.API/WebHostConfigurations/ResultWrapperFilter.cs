using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication.Application.Shared.Exceptions;
using WebApplication.Application.Shared.Models;

namespace WebApplication.API.WebHostConfigurations
{
	public class ResultWrapperFilter : ActionFilterAttribute, IExceptionFilter
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Result is not null)
			{
				var objectResult = (context.Result as ObjectResult).Value;
				var result = WebAppResult.SuccessResult(objectResult);
				context.Result = new OkObjectResult(result);
			}

			base.OnActionExecuted(context);
		}

		public void OnException(ExceptionContext context)
		{
			if (context.Exception is WebAppApplicationException ex)
			{
				var result = WebAppResult.FailedResult(ex.ErrorCode, ex.Message);
				context.Result = CreateErrorResult(ex.ErrorCode, result);

				context.ExceptionHandled = true;
			}

			IActionResult CreateErrorResult(WebAppErrorCode errorCode, WebAppResult<object> result)
			{
				return errorCode switch
				{
					WebAppErrorCode.EntityNotFound => new NotFoundObjectResult(result),

					_ => new BadRequestObjectResult(result),
				};
			}
		}
	}
}
