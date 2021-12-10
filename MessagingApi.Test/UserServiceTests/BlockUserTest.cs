using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.UserServiceTests
{
    public class BlockUserTest : BaseUserTest
    {
        [Fact]
        public async void Should_BlockUser()
        {
            //Arrange
            int id = 1;
            var user = new User() { Id = id, Blocked = false };

            mockManager.Setup(x => x.FindByIdAsync(id.ToString())).ReturnsAsync(user);
            mockManager.Setup(x => x.UpdateAsync(user)).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            await service.BlockUserById(id);

            //Assert
            Assert.True(user.Blocked);
            mockManager.Verify(x => x.UpdateAsync(user), Times.Once);
            mockManager.Verify(x => x.FindByIdAsync(id.ToString()), Times.Once);
        }
    }
}
