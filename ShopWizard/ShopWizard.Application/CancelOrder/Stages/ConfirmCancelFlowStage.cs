using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CancelOrder.Commands;
using ShopWizard.Application.CancelOrder.Enums;
using ShopWizard.Application.CancelOrder.Interfaces;
using System.Threading.Tasks;

namespace ShopWizard.Application.CancelOrder.Stages
{
	public class ConfirmCancelFlowStage : ICancelOrderFlowStage
	{
		private readonly ICancelOrderAppService _appService;
		private readonly ICancelOrderOutputPort _outputPort;

		public string FlowStageName => CancelOrderFlowStageType.ConfirmCancel.ToString();

		public ConfirmCancelFlowStage(ICancelOrderAppService appService, ICancelOrderOutputPort outputPort)
		{
			_appService = appService;
			_outputPort = outputPort;
		}

		public Task<string> ProcessCommand(CancelOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				SubmitCancelOrderCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<string> DoProcessing(CancelOrderFlowContext context, SubmitCancelOrderCommand command)
		{
			if (!command.ShouldCancelOrder.HasValue)
			{
				context.ErrorMessage = "Chose option";

				var viewModel = await _appService.GetConfirmationPage(context);
				_outputPort.ConfirmCancel(viewModel);

				return CancelOrderFlowStageType.ConfirmCancel.ToString();
			}

			if (command.ShouldCancelOrder == false)
			{
				_outputPort.Home();

				return CancelOrderFlowStageType.Сomplete.ToString();
			}

			// Perform action
			var summaryViewModel = await _appService.CancelOrder(context);

			// Show new view
			_outputPort.Summary(summaryViewModel);

			// Switch to next stage
			return CancelOrderFlowStageType.Сomplete.ToString();
		}
	}
}
