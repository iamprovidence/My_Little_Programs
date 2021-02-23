using FlowStage.Abstractions.Interfaces;
using System;
using System.Collections.Concurrent;

namespace FlowStage
{
	public class OutputPortFactory : IOutputPortFactory
	{
		private readonly ConcurrentDictionary<Type, object> _instances = new ConcurrentDictionary<Type, object>();

		public TOutputPort MakeInstance<TOutputPort>() where TOutputPort : IOutputPort
		{
			return (TOutputPort)_instances[typeof(TOutputPort)];
		}

		public void Register<TOutputPort>(TOutputPort outputPort) where TOutputPort : IOutputPort
		{
			_instances[typeof(TOutputPort)] = outputPort;
		}
	}
}
