using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CancelOrder.Commands;
using ShopWizard.Application.CancelOrder.Enums;
using ShopWizard.Application.CancelOrder.Interfaces;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages
{
	public class EnterOrderCodeFlowStage : ICancelOrderFlowStage
	{
		private readonly ICancelOrderAppService _appService;
		private readonly ICancelOrderOutputPort _outputPort;

		public string FlowStageName => CancelOrderFlowStageType.EnderOrderCode.ToString();

		public EnterOrderCodeFlowStage(ICancelOrderAppService appService, ICancelOrderOutputPort outputPort)
		{
			_appService = appService;
			_outputPort = outputPort;
		}

		public Task<string> ProcessCommand(CancelOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				GoToEnterOrderCodeCommand goToEnterOrderCodeCommand =>  DoProcessing(context, goToEnterOrderCodeCommand),
				SubmitOrderCodeCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<string> DoProcessing(CancelOrderFlowContext context, GoToEnterOrderCodeCommand command)
		{
			var viewModel = await _appService.GetEnterOrderCodePage(context);
			_outputPort.EnterOrderCode(viewModel);

			return CancelOrderFlowStageType.EnderOrderCode.ToString();
		}

		private async Task<string> DoProcessing(CancelOrderFlowContext context, SubmitOrderCodeCommand command)
		{
			if (string.IsNullOrWhiteSpace(command?.OrderCode))
			{
				context.ErrorMessage = "Enter order code";

				var viewModel = await _appService.GetEnterOrderCodePage(context, command);
				_outputPort.EnterOrderCode(viewModel);

				return CancelOrderFlowStageType.EnderOrderCode.ToString();
			}

			if (!await _appService.DoesOrderExist(command.OrderCode))
			{
				context.ErrorMessage = "Order not found. Enter another code";

				var viewModel = await _appService.GetEnterOrderCodePage(context, command);
				_outputPort.EnterOrderCode(viewModel);

				return CancelOrderFlowStageType.EnderOrderCode.ToString();
			}

			// Commit changes
			context.OrderCode = command.OrderCode;

			// Show new view
			var confirmationViewModel = await _appService.GetConfirmationPage(context);
			_outputPort.ConfirmCancel(confirmationViewModel);

			// Switch to next stage
			return CancelOrderFlowStageType.ConfirmCancel.ToString();
		}
	}
}
