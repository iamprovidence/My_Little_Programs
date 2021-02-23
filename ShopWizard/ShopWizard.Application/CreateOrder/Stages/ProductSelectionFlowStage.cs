using FlowStage.Abstractions.Exceptions;
using FlowStage.Abstractions.Interfaces;
using ShopWizard.Application.CancelOrder.Interfaces;
using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.Enums;
using ShopWizard.Application.CreateOrder.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages
{
	public class ProductSelectionFlowStage : ICreateOrderFlowStage
	{
		private readonly ICreateOrderAppService _appService;
		private readonly IOutputPortFactory _outputPortFactory;
		private ICreateOrderOutputPort _outputPort => _outputPortFactory.MakeInstance<ICreateOrderOutputPort>();

		public string FlowStageName => CreateOrderFlowStageType.ProductSelection.ToString();

		public ProductSelectionFlowStage(ICreateOrderAppService appService, IOutputPortFactory outputPortFactory)
		{
			_appService = appService;
			_outputPortFactory = outputPortFactory;
		}

		public Task<string> ProcessCommand(CreateOrderFlowContext context, IFlowCommand command)
		{
			return command switch
			{
				GoToProductSelectionCommand goToProductSelectionCommand => DoProcessing(context, goToProductSelectionCommand),
				SubmitProductSelectionCommand submitCommand => DoProcessing(context, submitCommand),

				_ => throw new UnsupportedCommandException(),
			};
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, GoToProductSelectionCommand command)
		{
			var viewModel = await _appService.GetProductListPage(context, command);
			_outputPort.ProductSelection(viewModel);

			return CreateOrderFlowStageType.ProductSelection.ToString();
		}

		private async Task<string> DoProcessing(CreateOrderFlowContext context, SubmitProductSelectionCommand command)
		{
			Console.WriteLine("Stage1 processing...");

			// Validation command
			if (command == null)
			{
				var viewModel = await _appService.GetProductListPage(context, command);
				_outputPort.ProductSelection(viewModel);

				return CreateOrderFlowStageType.ProductSelection.ToString();
			}

			if (!IsCommandValid(command))
			{
				context.ErrorMessage = "Select product";

				var viewModel = await _appService.GetProductListPage(context, command);
				_outputPort.ProductSelection(viewModel);

				return CreateOrderFlowStageType.ProductSelection.ToString();
			}

			// Commit changes
			context.Product = command.Product;

			// Show new view
			var contactDetailsViewModel = await _appService.GetContactDetailsPage(context, command);
			_outputPort.ContactDetails(contactDetailsViewModel);

			// Switch to Stage2
			return CreateOrderFlowStageType.ContactDetails.ToString();
		}

		private bool IsCommandValid(SubmitProductSelectionCommand command)
		{
			return command.Product.HasValue;
		}

	}
}
