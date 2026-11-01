using FlowStage.Interfaces;
using FlowStage.Models;
using ShopWizard.Application.CreateOrder.Stages.ContactDetails;
using ShopWizard.Application.CreateOrder.Stages.ProductSelection;
using ShopWizard.Domain.Enums;
using System.Threading.Tasks;

namespace ShopWizard.Application.CreateOrder
{
    public class CreateOrderFlowStart : IFlowStart<CreateOrderFlowContext>
    {
        // Hardcoded for now. Set a product to skip product selection and start from contact details
        private static readonly ProductType? PreselectedProduct = null;

        public Task<FlowStageIdentifier> Start(CreateOrderFlowContext context)
        {
            if (PreselectedProduct.HasValue)
            {
                // A skipped stage still has to provide its data
                context.Product = PreselectedProduct;

                return Task.FromResult(FlowStageIdentifier.From<ContactDetailsFlowStage>());
            }

            return Task.FromResult(FlowStageIdentifier.From<ProductSelectionFlowStage>());
        }
    }
}
