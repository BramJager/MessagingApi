using MessagingApi.Domain.Objects;
using Moq;
using Xunit;

namespace MessagingApi.Test.UserServiceTests
{
    public class GetUserTests : BaseUserTest
    {
        public GetUserTests() : base() { }

        [Fact]
        public async void Should_GetUserById()
        {
            //Arrange
            int id = 1;
            mockManager.Setup(x => x.FindByIdAsync(id.ToString())).ReturnsAsync(new User() { Id = 1});

            //Act
            var user = await service.GetUserById(id);

            //Assert
            mockManager.Verify(x => x.FindByIdAsync(id.ToString()), Times.Once);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        public async void Should_GetUserByUsername()
        {
            //Arrange
            string username = "test";
            mockManager.Setup(x => x.FindByNameAsync(username)).ReturnsAsync(new User() { UserName = username });

            //Act
            var user = await service.GetUserByUsername(username);

            //Assert
            mockManager.Verify(x => x.FindByNameAsync(username), Times.Once);
            Assert.Equal(username, user.UserName);
        }
    }
}
