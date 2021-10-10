using Products.Application.Contracts.CreateProduct;
using Products.Application.Contracts.DeleteProduct;

namespace Products.Application.Contracts
{
	public interface IProductApiClient
	{
		void Execute(CreateProductCommand command);
		void Execute(DeleteProductCommand command);
	}
}
