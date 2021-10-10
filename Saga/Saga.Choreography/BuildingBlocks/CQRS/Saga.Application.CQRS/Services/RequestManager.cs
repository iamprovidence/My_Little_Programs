using Saga.Application.CQRS.Abstractions;
using Saga.Application.CQRS.Models;
using System;
using System.Collections.Generic;

namespace Saga.Application.CQRS.Services
{
	public class RequestManager : IRequestManager
	{
		private readonly ISet<string> _processedCommands = new HashSet<string>();

		public void Validate<TRequest>(TRequest command) where TRequest : IdempotentCommand
		{
			if (_processedCommands.Contains(command.CommandId))
			{
				throw new InvalidOperationException("Command is already processed");
			}

			_processedCommands.Add(command.CommandId);
		}
	}
}
