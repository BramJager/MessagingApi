using MessagingApi.Domain.Objects;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.UserServiceTests
{
    public class UserLoginTest : BaseUserTest
    {
        public UserLoginTest() : base() { }

        [Fact]
        public async void Should_ReturnTrueOnValidLogin()
        {
            //Arrange
            var username = "test";
            var password = "test";
            mockManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            mockManager.Setup(x => x.FindByNameAsync(username)).Returns(Task.FromResult(new User() { UserName = "test" }));

            //Act
            var result = await service.CheckLogin(username, password);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void Should_ReturnJWTWhenAValidUserLogsIn()
        {
            //Arrange
            var username = "test";
            mockManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            mockManager.Setup(x => x.FindByNameAsync(username)).Returns(Task.FromResult(new User() { UserName = "test" }));
            mockManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>() { "Admin" });

            //Act
            var jwt = await service.GenerateJWTForUsername(username);

            //Assert
            Assert.NotNull(jwt);
        }

        [Fact]
        public async void Should_ReturnFalseOnInvalidLogin()
        {
            //Arrange
            var username = "test";
            var password = "test";
            mockManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            mockManager.Setup(x => x.FindByNameAsync(username)).Returns(Task.FromResult(new User() { UserName = "test" }));

            //Act
            var result = await service.CheckLogin(username, password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Should_ThrowException_When_UserDoesNotExist()
        {
            //Arrange
            var username = "test";
            var password = "test";
            mockManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            mockManager.Setup(x => x.FindByNameAsync(username)).Returns(Task.FromResult<User>(null));

            //Act
            Task result() => service.CheckLogin(username, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }
    }
}
