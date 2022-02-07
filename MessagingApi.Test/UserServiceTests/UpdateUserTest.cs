using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.UserServiceTests
{
    public class UpdateUserTest : BaseUserTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Should_UpdatUserWithoutPassword(string password)
        {
            //Arrange
            int id = 1;
            string updatedEmail = "test1@test.nl";

            var updatedUser = new User() {Id = id, Email = updatedEmail };

            mockManager.Setup(x => x.UpdateAsync(updatedUser)).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            await service.UpdateUser(updatedUser, password);

            //Assert
            mockManager.Verify(x => x.UpdateAsync(updatedUser), Times.Once);
            mockManager.Verify(x => x.GeneratePasswordResetTokenAsync(updatedUser), Times.Never);
            Assert.Equal(updatedUser.Email, updatedEmail);
        }

        [Fact]
        public async void Should_UpdateUserWithPassword()
        {
            //Arrange
            int id = 1;
            string updatedEmail = "test1@test.nl";
            string password = "test";
            string token = "test";

            var updatedUser = new User() { Id = id, Email = updatedEmail };

            mockManager.Setup(x => x.UpdateAsync(updatedUser)).Returns(Task.FromResult(IdentityResult.Success));
            mockManager.Setup(x => x.GeneratePasswordResetTokenAsync(updatedUser)).ReturnsAsync(token);
            mockManager.Setup(x => x.ResetPasswordAsync(updatedUser, token, password));

            //Act
            await service.UpdateUser(updatedUser, password);

            //Assert
            mockManager.Verify(x => x.UpdateAsync(updatedUser), Times.Once);
            mockManager.Verify(x => x.GeneratePasswordResetTokenAsync(updatedUser), Times.Once);
            mockManager.Verify(x => x.ResetPasswordAsync(updatedUser, token, password), Times.Once);
            Assert.Equal(updatedUser.Email, updatedEmail);
        }
    }
}
