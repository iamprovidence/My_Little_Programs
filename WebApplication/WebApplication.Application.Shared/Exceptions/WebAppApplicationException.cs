using System;

namespace WebApplication.Application.Shared.Exceptions
{
	public class WebAppApplicationException : ApplicationException
	{
		public WebAppApplicationException(WebAppErrorCode errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
		}

		public WebAppErrorCode ErrorCode { get; }
	}
}
