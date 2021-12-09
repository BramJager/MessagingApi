using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.UserServiceTests
{
    public class UserRegistrationTest : BaseUserTest
    {
        public UserRegistrationTest() : base() { }

        [Fact]
        public async void Should_RegisterNewUser()
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            mockManager.Setup(x => x.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));

            var user = new User() { Email = "test@test.nl", UserName = "test", FirstName = "test", Surname = "test" };
            var password = "test";

            //Act
            await service.RegisterUser(user, password);

            //Assert
            mockManager.Verify(x => x.CreateAsync(user, password), Times.Once);
            mockManager.Verify(x => x.UpdateSecurityStampAsync(user), Times.Once);
            mockManager.Verify(x => x.AddToRoleAsync(user, "User"), Times.Once);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Should_ThrowArgumentException_When_UsernameIsNullOrEmpty(string username)
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            var user = new User() { Email = "test@test.nl", UserName = username, FirstName = "test", Surname = "test" };
            var password = "test";

            //Act
            Task result() => service.RegisterUser(user, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Should_ThrowArgumentException_When_PasswordIsNullOrEmpty(string password)
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            var user = new User() { Email = "test@test.nl", UserName = "test", FirstName = "test", Surname = "test" };

            //Act
            Task result() => service.RegisterUser(user, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Theory]
        [InlineData("", "test")]
        [InlineData(null, "test")]
        [InlineData("test", "")]
        [InlineData("test", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        public async void Should_ThrowArgumentException_When_NameIsNullOrEmpty(string firstname, string surname)
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            var user = new User() { Email = "test@test.nl", UserName = "test", FirstName = firstname, Surname = surname };
            var password = "test";

            //Act
            Task result() => service.RegisterUser(user, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Should_ThrowArgumentException_When_EmailIsNullOrEmpty(string email)
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            var user = new User() { Email = email, UserName = "test", FirstName = "test", Surname = "test" };
            var password = "test";

            //Act
            Task result() => service.RegisterUser(user, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async void Should_ThrowArgumentException_When_EmailIsNotUnique()
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            mockManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(new User {Email = "test@test.nl"}));
        
            var user = new User() { Email = "test@test.nl", UserName = "test", FirstName = "test", Surname = "test" };
            var password = "test";
        
            //Act
            Task result() => service.RegisterUser(user, password);
        
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async void Should_ThrowArgumentException_When_UsernameIsNotUnique()
        {
            //Arrange
            mockOptions.Setup(x => x.Value).Returns(options);

            mockManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new User()));

            var user = new User() { Email = "test@test.nl", UserName = "test", FirstName = "test", Surname = "test" };
            var password = "test";

            //Act
            Task result() => service.RegisterUser(user, password);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }
    }
}
