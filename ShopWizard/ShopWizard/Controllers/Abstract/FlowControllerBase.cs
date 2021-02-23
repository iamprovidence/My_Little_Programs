using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ShopWizard.Controllers.Abstract
{
	public abstract class FlowControllerBase<TContext> : Controller
		where TContext : IFlowContext, new()
	{
		private IActionResult _actionResult;

		private readonly IFlowService<TContext> _flowService;

		public FlowControllerBase(IFlowService<TContext> flowService)
		{
			_flowService = flowService;
		}

		public void Return(IActionResult actionResult)
		{
			_actionResult = actionResult;
		}

		public async Task<IActionResult> ProcessCommand(string flowState, IFlowCommand command)
		{
			var context = GetFlowContext(flowState);

			await _flowService.ProcessCommand(context, command);

			if (_actionResult == null)
			{
				throw new EmptyOutputPortException();
			}

			return _actionResult;
		}

		private TContext GetFlowContext(string flowState)
		{
			if (string.IsNullOrWhiteSpace(flowState))
			{
				return new TContext();
			}

			return JsonConvert.DeserializeObject<TContext>(flowState);
		}

	}
}
