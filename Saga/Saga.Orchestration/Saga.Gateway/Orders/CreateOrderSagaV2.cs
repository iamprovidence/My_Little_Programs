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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saga.Gateway.Orders
{
	public class CreateOrderSagaV2 : ISagaCoordinator<CreateOrderCommand, OrderItem>
	{
		private class TransactionNode
		{
			public Func<Task> Action { get; set; }
			public Func<Task> Compensation { get; set; }
		}

		private readonly IOrderApiClient _orderApiClient;
		private readonly IProductApiClient _productApiClient;
		private readonly IMessageApiClient _messageApiClient;

		private CreateOrderCommand _request;

		public CreateOrderSagaV2(
			IOrderApiClient orderApiClient,
			IProductApiClient productApiClient,
			IMessageApiClient messageApiClient
			)
		{
			_orderApiClient = orderApiClient;
			_productApiClient = productApiClient;
			_messageApiClient = messageApiClient;
		}

		public async Task<OrderItem> Process(CreateOrderCommand request)
		{
			_request = request;

			var currentNode = GetFirstTransactionNode();

			try
			{
				for (var node = currentNode; node != null; currentNode = currentNode.Next)
				{
					await node.Value.Action.Invoke();
				}

				return _orderApiClient.Query(new GetOrderByIdQuery()
				{
					OrderId = _request.OrderId,
				});
			}
			catch
			{
				for (var node = currentNode; node != null; currentNode = currentNode.Previous)
				{
					await node.Value.Compensation.Invoke();
				}

				return null;
			}
		}

		private LinkedListNode<TransactionNode> GetFirstTransactionNode()
		{
			var transactionNodes = new LinkedList<TransactionNode>();

			transactionNodes.AddLast(new TransactionNode
			{
				Action = CreateOrder,
				Compensation = DeleteOrder,
			});

			transactionNodes.AddLast(new TransactionNode
			{
				Action = CreateProduct,
				Compensation = DeleteProduct,
			});

			transactionNodes.AddLast(new TransactionNode
			{
				Action = SendMessage,
				Compensation = () => Task.CompletedTask,
			});

			return transactionNodes.First;
		}

		private Task CreateOrder()
		{
			_orderApiClient.Execute(new CreateOrderCommand());

			return Task.CompletedTask;
		}

		private Task DeleteOrder()
		{
			_orderApiClient.Execute(new DeleteOrderCommand());

			return Task.CompletedTask;
		}

		private Task CreateProduct()
		{
			_productApiClient.Execute(new CreateProductCommand()
			{
				OrderId = _request.OrderId,
			});

			return Task.CompletedTask;
		}

		private Task DeleteProduct()
		{
			_productApiClient.Execute(new DeleteProductCommand());

			return Task.CompletedTask;
		}

		private async Task SendMessage()
		{
			await _messageApiClient.Execute(new SendSmsCommand());
		}
	}
}
