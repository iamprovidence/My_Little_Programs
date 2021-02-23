using ShopWizard.Application.CancelOrder.Commands;
using ShopWizard.Application.CancelOrder.Interfaces;
using ShopWizard.Application.CancelOrder.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder
{
	public class CancelOrderAppService : ICancelOrderAppService
	{
		public Task<SummaryViewModel> CancelOrder(CancelOrderFlowContext context)
		{
			var didSucceed = new Random().NextDouble() > 0.5;

			var viewModel = new SummaryViewModel
			{
				FlowContext = context,
				DidSucceed = didSucceed,
			};

			return Task.FromResult(viewModel);
		}

		public Task<bool> DoesOrderExist(string orderCode)
		{
			var doesOrderExit = orderCode == "1111";

			return Task.FromResult(doesOrderExit);
		}

		public Task<ConfirmCancelViewModel> GetConfirmationPage(CancelOrderFlowContext context)
		{
			var viewModel = new ConfirmCancelViewModel
			{
				FlowContext = context,
				ShouldCancelOrder = null,
			};

			return Task.FromResult(viewModel);
		}

		public Task<EnterOrderCodeViewModel> GetEnterOrderCodePage(CancelOrderFlowContext context)
		{
			var viewModel = new EnterOrderCodeViewModel
			{
				FlowContext = context,
				OrderCode = string.Empty,
			};

			return Task.FromResult(viewModel);
		}
		public Task<EnterOrderCodeViewModel> GetEnterOrderCodePage(CancelOrderFlowContext context, SubmitOrderCodeCommand command)
		{
			var viewModel = new EnterOrderCodeViewModel
			{
				FlowContext = context,
				OrderCode = command?.OrderCode ?? context.OrderCode,
			};

			return Task.FromResult(viewModel);
		}
	}
}
