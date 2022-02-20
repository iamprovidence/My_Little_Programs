using System.Collections.Generic;
using WebArchitecture.Application.Shared.Exceptions;

namespace WebArchitecture.Application.Shared.Models
{
	public record WebAppResult<TData>
	{
		public bool IsSuccessful { get; init; }
		public TData Data { get; init; }
		public IReadOnlyCollection<string> ErrorMessages { get; init; } = new List<string>();
		public IReadOnlyCollection<WebAppErrorCode> ErrorCodes { get; init; } = new List<WebAppErrorCode>();
	}

	public record WebAppResult : WebAppResult<object>
	{
		public static WebAppResult<T> SuccessResult<T>(T data)
		{
			return new WebAppResult<T>
			{
				IsSuccessful = true,
				Data = data,
			};
		}

		public static WebAppResult<object> FailedResult(WebAppErrorCode errorCode, string message)
		{
			return new WebAppResult<object>
			{
				IsSuccessful = false,
				ErrorCodes = new List<WebAppErrorCode> { errorCode },
				ErrorMessages = new List<string> { message },
			};
		}
	}
}
