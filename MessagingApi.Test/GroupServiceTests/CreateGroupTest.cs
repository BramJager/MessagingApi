using MessagingApi.Domain.Objects;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.GroupServiceTests
{
    public class CreateGroupTest : BaseGroupTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Should_ThrowArgumentNullException_When_GroupNameIsNullOrEmpty(string name)
        {
            //Arrange
            Group group = new Group() { Name = name};

            //Act
            Task result() => service.CreateGroup(group);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact]
        public async void Should_ThrowArgumentNullException_When_GroupIsNull()
        {
            //Arrange
            Group group = null;

            //Act
            Task result() => service.CreateGroup(group);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Should_ThrowArgumentNullException_When_GroupIsPrivate_And_PasswordHashIsNullOrEmpty(string password)
        {
            //Arrange
            Group group = new Group() { Name = "test", Visibility = Visibility.Private, PasswordHash = password};

            //Act
            Task result() => service.CreateGroup(group);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact]
        public async void Should_CreateGroup_When_PrivateAndWithPasswordHash()
        {
            //Arrange
            Group group = new Group() { Name = "test", Visibility = Visibility.Private, PasswordHash = "HASH" };
            mockRepository.Setup(x => x.Add(group));

            //Act
            await service.CreateGroup(group);

            //Assert
            mockRepository.Verify(x => x.Add(group), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Should_CreateGroup_When_PublicAndPasswordHashIsNullOrEmpty(string password)
        {
            //Arrange
            Group group = new Group() { Name = "test", Visibility = Visibility.Public, PasswordHash = password };
            mockRepository.Setup(x => x.Add(group));

            //Act
            await service.CreateGroup(group);

            //Assert
            mockRepository.Verify(x => x.Add(group), Times.Once);
        }
    }
}
