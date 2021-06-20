using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace WebApplication.Infrastructure.OpenApi.Swagger.SchemaFilters
{
	internal class EnumSchemaFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context.Type.IsEnum)
			{
				schema.Enum.Clear();

				foreach (var enumName in Enum.GetNames(context.Type))
				{
					var enumSchemaName = $"{Convert.ToInt64(Enum.Parse(context.Type, enumName))} - {enumName}";

					schema.Enum.Add(new OpenApiString(enumSchemaName));
				}
			}
		}
	}
}
