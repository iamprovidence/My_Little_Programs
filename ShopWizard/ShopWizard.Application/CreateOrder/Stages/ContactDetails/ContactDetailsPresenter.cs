using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails.ViewModels;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder.Stages.ContactDetails
{
	public class ContactDetailsPresenter : ICreateOrderPresenter<ContactDetailsViewModel>
	{
		public FlowStageIdentifier FlowStageIdentifier => FlowStageIdentifier.From<ContactDetailsFlowStage>();

		public Task<ContactDetailsViewModel> ShowView(CreateOrderFlowContext context)
		{
			var viewModel = new ContactDetailsViewModel
			{
				FlowContext = context,
				Email = context.Email,
			};

			return Task.FromResult(viewModel);
		}
	}
}
