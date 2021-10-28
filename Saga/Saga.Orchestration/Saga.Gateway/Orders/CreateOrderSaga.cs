using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;
using Messages.Application.Contracts;
using Messages.Application.Contracts.SendSms;
using Orders.Application.Contracts;
using Orders.Application.Contracts.CreateOrder;
using Orders.Application.Contracts.DeleteOrder;
using Orders.Application.Contracts.GetOrderById;
using Products.Application.Contracts;
using Products.Application.Contracts.CreateProduct;
using Products.Application.Contracts.DeleteProduct;
using System;
using System.Threading.Tasks;

namespace Saga.Gateway.Orders
{
	internal enum State
	{
		Initial,
		OrderCreated,
		ProductCreated,
		MessageSend,
		Completed,
		Failed,
	}

	internal enum Event
	{
		CreateOrder,
		CreateProduct,
		SendMessage,
		Complete,
		Fail,
	}

	public class CreateOrderSaga : ISagaCoordinator<CreateOrderCommand, OrderItem>
	{
		private readonly IAsyncStateMachine<State, Event> _stateMachine;

		private readonly IOrderApiClient _orderApiClient;
		private readonly IProductApiClient _productApiClient;
		private readonly IMessageApiClient _messageApiClient;

		private readonly TaskCompletionSource<OrderItem> _taskResult;
		private CreateOrderCommand _request;

		public CreateOrderSaga(
			IOrderApiClient orderApiClient,
			IProductApiClient productApiClient,
			IMessageApiClient messageApiClient
			)
		{
			_stateMachine = ConfigureStateMachine();

			_taskResult = new TaskCompletionSource<OrderItem>();

			_orderApiClient = orderApiClient;
			_productApiClient = productApiClient;
			_messageApiClient = messageApiClient;
		}

		private IAsyncStateMachine<State, Event> ConfigureStateMachine()
		{
			var builder = new StateMachineDefinitionBuilder<State, Event>();

			builder
				.WithInitialState(State.Initial);

			builder
				.In(State.Initial)
					.On(Event.CreateOrder)
						.Goto(State.OrderCreated)
						.Execute(OnCreateOrder)
					.On(Event.Fail)
						.Goto(State.Failed)
						.Execute(OnFailed);

			builder
				.In(State.OrderCreated)
					.On(Event.CreateProduct)
						.Goto(State.ProductCreated)
						.Execute(OnCreateProduct)
					.On(Event.Fail)
						.Goto(State.Initial)
						.Execute(OnCreateOrderFailed);

			builder
				.In(State.ProductCreated)
					.On(Event.SendMessage)
						.Goto(State.MessageSend)
						.Execute(OnSendMessage)
					.On(Event.Fail)
						.Goto(State.OrderCreated)
						.Execute(OnCreateProductFailed);

			builder
				.In(State.MessageSend)
					.On(Event.Complete)
						.Goto(State.Completed)
						.Execute(OnCompleted)
					.On(Event.Fail)
						.Goto(State.ProductCreated)
						.Execute(OnSendMessageFailed);

			return builder.Build().CreatePassiveStateMachine();
		}

		public async Task<OrderItem> Process(CreateOrderCommand request)
		{
			_request = request;

			await _stateMachine.Start();
			await _stateMachine.Fire(Event.CreateOrder);
			await _stateMachine.Stop();

			return await _taskResult.Task;
		}

		private async Task OnCreateOrder()
		{
			try
			{
				_orderApiClient.Execute(new CreateOrderCommand());

				await _stateMachine.Fire(Event.CreateProduct);
			}
			catch
			{
				await _stateMachine.Fire(Event.Fail);
			}
		}

		private async Task OnCreateOrderFailed()
		{
			await _stateMachine.Fire(Event.Fail);
		}

		private async Task OnCreateProduct()
		{
			try
			{
				_productApiClient.Execute(new CreateProductCommand()
				{
					OrderId = _request.OrderId,
				});

				await _stateMachine.Fire(Event.SendMessage);
			}
			catch
			{
				await _stateMachine.Fire(Event.Fail);
			}
		}

		private async Task OnCreateProductFailed()
		{
			_orderApiClient.Execute(new DeleteOrderCommand());

			await _stateMachine.Fire(Event.Fail);
		}

		private async Task OnSendMessage()
		{
			try
			{
				await _messageApiClient.Execute(new SendSmsCommand());

				await _stateMachine.Fire(Event.Complete);
			}
			catch (Exception)
			{
				await _stateMachine.Fire(Event.Fail);
			}
		}

		private async Task OnSendMessageFailed()
		{
			_productApiClient.Execute(new DeleteProductCommand());

			await _stateMachine.Fire(Event.Fail);
		}

		private void OnFailed()
		{
			_taskResult.SetResult(null);
		}

		private void OnCompleted()
		{
			var order = _orderApiClient.Query(new GetOrderByIdQuery()
			{
				OrderId = _request.OrderId,
			});

			_taskResult.SetResult(order);
		}
	}
}
