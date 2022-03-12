using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebArchitecture.Tests
{
	internal static class AssertHelper
	{
		public static void AssertDependencies(string namespaceRoot, IServiceCollection serviceCollection)
		{
			var services = serviceCollection.Select(x => x.ServiceType).ToHashSet();
			var servicesToValidate = GetServicesToValidate(namespaceRoot, serviceCollection);

			var errors = new List<string>();
			foreach (var service in servicesToValidate)
			{
				var constructorParameters = service
					.GetConstructors()
					.SelectMany(x => x.GetParameters())
					.Select(x => x.ParameterType)
					.Distinct();

				foreach (var parameter in constructorParameters)
				{
					if (!services.Contains(parameter))
					{
						errors.Add($"Cannot resolve '{service.Name}'. Missing '{parameter.Name}'");
					}
				}
			}

			if (errors.Any())
			{
				throw new InvalidOperationException(string.Join(Environment.NewLine, errors));
			}
		}

		private static IEnumerable<Type> GetServicesToValidate(string namespaceRoot, IEnumerable<ServiceDescriptor> services)
		{
			var servicesToValidate = services
				.Select(s => s.ServiceType)
				.Where(s => s.IsClass)
				.Where(s => s.Namespace.StartsWith(namespaceRoot))
				.ToList();

			var implementationsToValidate = services
				.Where(s => s.ImplementationType != null)
				.Select(s => s.ImplementationType)
				.Where(s => s.Namespace.StartsWith(namespaceRoot))
				.ToList();

			var factoriesToValidate = services
				.Where(s => s.ImplementationFactory != null)
				.Where(s => s.ImplementationFactory.Method.ReturnType.Namespace.StartsWith(namespaceRoot))
				.Select(s => s.ImplementationFactory.Method.ReflectedType.GenericTypeArguments)
				.Where(s => s.Length == 2)
				.Select(s => s[1])
				.ToList();

			return servicesToValidate.Union(implementationsToValidate).Union(factoriesToValidate);
		}
	}
}
