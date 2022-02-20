using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebArchitecture.Application.ApiClient.Abstractions;
using WebArchitecture.Application.Shared.Exceptions;
using WebArchitecture.Application.Shared.Models;
using WebArchitecture.Application.ViewModels.TodoItems;

namespace WebArchitecture.Infrastructure.ApiClient.Http.Services
{
	internal class WebArchitectureHttpClient : IWebArchitectureClient
	{
		private readonly HttpClient _httpClient;

		public WebArchitectureHttpClient(HttpClient httpClient)
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
				throw new WebAppArchitectureException(WebAppErrorCode.Undefined, "API response data could not be interpreted!");
			}

			if (!result.IsSuccessful)
			{
				if (result.ErrorCodes.Any())
				{
					var errorCode = result.ErrorCodes.First();
					var errorMessage = result.ErrorMessages.FirstOrDefault() ?? errorCode.ToString();

					throw new WebAppArchitectureException(errorCode, errorMessage);
				}
				else
				{
					throw new WebAppArchitectureException(WebAppErrorCode.Undefined, "API could not proceed the request!");
				}
			}

			return result.Data;
		}
	}
}
