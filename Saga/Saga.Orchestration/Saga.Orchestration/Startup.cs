using Messages.Application.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Moq;
using Orders.Application.Contracts;
using Orders.Application.Contracts.GetOrderById;
using Products.Application.Contracts;
using Saga.Gateway.Orders;

namespace Saga.Orchestration
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddScoped<CreateOrderSaga>();
			services.AddScoped(sp =>
			{
				var orderApiClientMock = new Mock<IOrderApiClient>();
				orderApiClientMock
					.Setup(x => x.Query(It.IsAny<GetOrderByIdQuery>()))
					.Returns<GetOrderByIdQuery>((query) => new OrderItem()
					{
						OrderId = query.OrderId,
					});

				return orderApiClientMock.Object;
			});
			services.AddScoped(sp => Mock.Of<IProductApiClient>());
			services.AddScoped(sp => Mock.Of<IMessageApiClient>());

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Saga.Orchestration",
					Version = "v1",
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saga.Orchestration v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
