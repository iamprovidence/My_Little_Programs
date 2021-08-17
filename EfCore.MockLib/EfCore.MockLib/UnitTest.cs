using EfCore.MockLib.Ef;
using EfCore.MockLib.Mock;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EfCore.MockLib
{
	public class UnitTest
	{
		[Fact]
		public async Task User_should_be_added()
		{
			// Arrange
			var addedEntityCount = 0;
			var expectedUserToAdd = new User();

			var dbContextMock = DbContextMockHelper.BuildDbContextMock()
				.SetupAddCallback<User>(AssertAddCallback)
				.SetupSaveCallback(() => ++addedEntityCount);

			var service = new Service(dbContextMock.Object);

			// Act
			await service.AddUser(expectedUserToAdd);

			// Assert
			dbContextMock.Verify(x => x.Add(expectedUserToAdd), Times.Once);
			dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);

			addedEntityCount.Should().BeGreaterThan(0);
			addedEntityCount.Should().Be(1);

			void AssertAddCallback(User actualUser)
			{
				Assert.Equal(expectedUserToAdd, actualUser);
			}
		}

		[Fact]
		public async Task Adult_users_should_be_returned()
		{
			// Arrange
			var dbContextMock = DbContextMockHelper.BuildDbContextMock()
				.MockDbSet(x => x.Users, GetUserCollection());

			var service = new Service(dbContextMock.Object);

			// Act
			var result = await service.GetAdultUsers();

			// Assert
			result.Should().HaveCount(2);
		}

		[Fact]

		public async Task Users_should_be_found_by_search_condition()
		{
			// Arrange
			var dbContextMock = DbContextMockHelper.BuildDbContextMock()
				.MockDbSet(x => x.Users, GetUserCollection());

			var service = new Service(dbContextMock.Object);

			// Act
			var result = await service.SearchUsers("J");

			// Assert
			result.Should().HaveCount(2);
		}

		private static ICollection<User> GetUserCollection()
		{
			return new[]
			{
				new User
				{
					Id = 1,
					Name = "John",
					Age = 20,
				},
				new User
				{
					Id = 2,
					Name = "Jane",
					Age = 20,
				},
				new User
				{
					Id = 3,
					Name = "Alice",
					Age = 8,
				},
				new User
				{
					Id = 4,
					Name = "Bob",
					Age = 8,
				},
			};
		}
	}
}
