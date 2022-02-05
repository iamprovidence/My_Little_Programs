using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.ApiClient.Abstractions;
using WebApplication.Application.Shared.Exceptions;
using WebApplication.Application.Shared.Models;
using WebApplication.Application.ViewModels.TodoItems;

namespace WebApplication.Infrastructure.ApiClient.Http.Services
{
	internal class WebApplicationHttpClient : IWebApplicationClient
	{
		private readonly HttpClient _httpClient;

		public WebApplicationHttpClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		#region TodoItem
		private string GetTodoItemBaseUrl(string url)
		{
			return $"api/todo-items/{url}";
		}

		public async Task<TodoItemViewModel> GetTodoItem(int todoItemId, CancellationToken cancellationToken = default)
		{
			var url = GetTodoItemBaseUrl(todoItemId.ToString());

			var response = await _httpClient.GetAsync(url, cancellationToken);
			var responseData = await ReadContentAs<TodoItemViewModel>(response, cancellationToken);

			return responseData;
		}
		#endregion

		private async Task<T> ReadContentAs<T>(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
		{
			var result = default(WebAppResult<T>);

			try
			{
				result = await httpResponse.Content.ReadAsAsync<WebAppResult<T>>(cancellationToken);
			}
			catch
			{
				throw new WebAppApplicationException(WebAppErrorCode.Undefined, "API response data could not be interpreted!");
			}

			if (!result.IsSuccessful)
			{
				if (result.ErrorCodes.Any())
				{
					var errorCode = result.ErrorCodes.First();
					var errorMessage = result.ErrorMessages.FirstOrDefault() ?? errorCode.ToString();

					throw new WebAppApplicationException(errorCode, errorMessage);
				}
				else
				{
					throw new WebAppApplicationException(WebAppErrorCode.Undefined, "API could not proceed the request!");
				}
			}

			return result.Data;
		}
	}
}
