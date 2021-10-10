using MediatR;
using Messages.Application.Contracts.SendSms;
using Messages.Application.SendSms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Orders.Application;
using Orders.Application.Contracts.CreateOrder;
using Orders.Application.CreateOrder;
using Products.Application.Contracts.CreateProduct;
using Products.Application.CreateProduct;
using Saga.Application.CQRS.Abstractions;
using Saga.Application.CQRS.Services;
using Saga.Application.EventBus.Abstractions;
using Saga.Application.EventBus.Models;
using Saga.Infrastructure.InMemoryEventBus;

namespace Saga.Choreography
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IEventBus, InMemoryEventBus>();
			services.AddSingleton<IEventStore, InMemoryEventStore>();

			services.AddSingleton<IRequestManager, RequestManager>();
			services.AddSingleton<IPendingTaskStore, PendingTaskStore>();
			services.AddScoped<IMessageDispatcher, MessageDispatcher>();

			services.AddMediatR(typeof(IAssemblyMarker));

			services.AddScoped<
				IIntegrationEventHandler<IntegrationEvent>,
				MessageDispatcher>();
			services.AddScoped<
				IIntegrationEventHandler<CreateOrderSucceededIntegrationEvent>,
				CreateProductOnCreateOrderSucceededIntegrationEventHandler>();
			services.AddScoped<
				IIntegrationEventHandler<CreateProductSucceededIntegrationEvent>,
				SendSmsOnCreateProductSucceededIntegrationEvent>();
			services.AddScoped<
				IIntegrationEventHandler<CreateProductFailedIntegrationEvent>,
				DeleteOrderOnCreateProductFailedIntegrationEventHandler>();
			services.AddScoped<
				IIntegrationEventHandler<SendSmsFailedIntegrationEvent>,
				DeleteProductOnSendSmsFailedIntegrationEventHandler>();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Saga.Choreography",
					Version = "v1",
				});
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saga.Choreography v1"));
			}

			ConfigureEventBus(app);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void ConfigureEventBus(IApplicationBuilder app)
		{
			var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

			eventBus.Subscribe<IntegrationEvent, IIntegrationEventHandler<IntegrationEvent>>();

			// orders
			eventBus.Subscribe<CreateOrderSucceededIntegrationEvent, IIntegrationEventHandler<CreateOrderSucceededIntegrationEvent>>();

			// products
			eventBus.Subscribe<CreateProductSucceededIntegrationEvent, IIntegrationEventHandler<CreateProductSucceededIntegrationEvent>>();
			eventBus.Subscribe<CreateProductFailedIntegrationEvent, IIntegrationEventHandler<CreateProductFailedIntegrationEvent>>();

			// messages
			eventBus.Subscribe<SendSmsSucceededIntegrationEvent, IIntegrationEventHandler<SendSmsSucceededIntegrationEvent>>();
			eventBus.Subscribe<SendSmsFailedIntegrationEvent, IIntegrationEventHandler<SendSmsFailedIntegrationEvent>>();

		}
	}
}
