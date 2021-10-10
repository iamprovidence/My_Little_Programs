using ShopWizard.Application.CreateOrder.Commands;
using ShopWizard.Application.CreateOrder.Interfaces;
using ShopWizard.Application.CreateOrder.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder
{
	// TODO: separate Presenter class for each stage
	public class CreateOrderAppService : ICreateOrderAppService
	{
		#region ContactDetails
		// TODO: remove command
		public Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, SubmitContactDetailsCommand command)
		{
			var viewModel = new ContactDetailsViewModel
			{
				FlowContext = context,
				Email = command?.Email ?? context.Email,
			};

			return Task.FromResult(viewModel);
		}

		public Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, GoToContactDetailsCommand command)
		{
			var viewModel = new ContactDetailsViewModel
			{
				FlowContext = context,
				Email = context.Email,
			};

			return Task.FromResult(viewModel);
		}

		public Task<ContactDetailsViewModel> GetContactDetailsPage(CreateOrderFlowContext context, SubmitProductSelectionCommand command)
		{
			var viewModel = new ContactDetailsViewModel
			{
				FlowContext = context,
				Email = context.Email,
			};

			return Task.FromResult(viewModel);
		}
		#endregion

		#region ProductList
		public Task<ProductSelectionViewModel> GetProductListPage(CreateOrderFlowContext context, SubmitProductSelectionCommand command)
		{
			var viewModel = new ProductSelectionViewModel
			{
				FlowContext = context,
				Product = command?.Product ?? context.Product,
			};

			return Task.FromResult(viewModel);
		}

		public Task<ProductSelectionViewModel> GetProductListPage(CreateOrderFlowContext context, GoToProductSelectionCommand command)
		{
			var viewModel = new ProductSelectionViewModel
			{
				FlowContext = context,
				Product = context.Product,
			};

			return Task.FromResult(viewModel);
		}
		#endregion

		#region PaymentPage
		public Task<PaymentDetailsViewModel> GetPaymentPage(CreateOrderFlowContext context, SubmitContactDetailsCommand command)
		{
			var viewModel = new PaymentDetailsViewModel
			{
				FlowContext = context,
				PaymentMethod = context.PaymentMethod,
			};

			return Task.FromResult(viewModel);
		}

		public Task<PaymentDetailsViewModel> GetPaymentPage(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command)
		{
			var viewModel = new PaymentDetailsViewModel
			{
				FlowContext = context,
				PaymentMethod = command?.PaymentMethod ?? context.PaymentMethod,
			};

			return Task.FromResult(viewModel);
		}
		#endregion

		#region SummaryPage
		public Task<SummaryPageViewModel> GetSummaryPage(CreateOrderFlowContext context, SubmitPaymentDetailsCommand command)
		{
			var reservationCode = new Random().Next(1111, 9999).ToString();

			var viewModel = new SummaryPageViewModel
			{
				FlowContext = context,
				OrderCode = reservationCode,
			};

			return Task.FromResult(viewModel);
		}

		public Task<SummaryPageViewModel> GetSummaryPage(CreateOrderFlowContext context)
		{
			var reservationCode = new Random().Next(1111, 9999).ToString();

			var viewModel = new SummaryPageViewModel
			{
				FlowContext = context,
				OrderCode = reservationCode,
			};

			return Task.FromResult(viewModel);
		}
		#endregion
	}
}
