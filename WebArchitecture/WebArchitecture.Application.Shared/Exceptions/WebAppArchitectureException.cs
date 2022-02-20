using System;

namespace WebArchitecture.Application.Shared.Exceptions
{
	public class WebAppArchitectureException : ApplicationException
	{
		public WebAppArchitectureException(WebAppErrorCode errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
		}

		public WebAppErrorCode ErrorCode { get; }
	}
}
