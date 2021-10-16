using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.Summary.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.Summary
{
	// HumbleObject
	public class SummaryPresenter : ICreateOrderPresenter<SummaryViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<SummaryFlowStage>();

		public Task<SummaryViewModel> ShowView(CreateOrderFlowContext context)
		{
			var orderCode = new Random().Next(1111, 9999).ToString();

			var viewModel = new SummaryViewModel
			{
				FlowContext = context,
				OrderCode = orderCode,
			};

			return Task.FromResult(viewModel);
		}
	}
}
