using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebArchitecture.Domain.Entities;
using WebArchitecture.Application.Shared.Exceptions;
using Xunit;

namespace WebArchitecture.IntegrationTests
{
	public class TodoItemControllerTests : IntegrationTestBase
	{
		public TodoItemControllerTests()
			: base() { }

		[Fact]
		public async Task TodoItem_should_be_returned()
		{
			// Arrange
			var client = GetTestServerBuilder()
				.MockData(new TodoItem
				{
					Id = 1,
					Description = "Test description",
				})
				.BuildServer();

			// Act
			var todoItem = await client.GetTodoItem(todoItemId: 1);

			// Assert
			todoItem.UserName.Should().Be("John Doe");
			todoItem.Description.Should().Be("Test description");
		}


		[Fact]
		public async Task TodoItem_should_not_be_found_when_searching_by_wrong_id()
		{
			// Arrange
			var client = GetTestServerBuilder()
				.BuildServer();

			// Act
			Func<Task> action = () => client.GetTodoItem(todoItemId: 69);

			// Assert
			(await action.Should()
				.ThrowAsync<WebAppArchitectureException>())
				.Where(x => x.ErrorCode == WebAppErrorCode.EntityNotFound);
		}
	}
}
