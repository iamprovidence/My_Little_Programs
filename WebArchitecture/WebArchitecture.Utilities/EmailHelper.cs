using System;
using System.Net.Mail;

namespace WebArchitecture.Utilities
{
	public static class EmailHelper
	{
		public static bool IsValid(string email)
		{
			try
			{
				new MailAddress(email);

				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
	}
}
