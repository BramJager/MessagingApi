using MessagingApi.Business;
using MessagingApi.Business.Data;
using MessagingApi.Business.Interfaces;
using MessagingApi.Business.Settings;
using MessagingApi.Domain.Models;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test
{
    public class UserTests
    {
        //[Fact]
        //public async void Should_RegisterNewUser()
        //{
        //    //Arrange
        //    var options = new JwtSettings()
        //    {
        //        Issuer = "issuer",
        //        Secret = "secret",
        //        ExpirationInMinutes = 60
        //    };
        //    var mockOptions = new Mock<IOptionsSnapshot<JwtSettings>>();
        //    mockOptions.Setup(x => x.Value).Returns(options);
        //    var context = new Mock<DataContext>();
        //
        //    var mockManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
        //
        //    var service = new UserService(mockManager.Object, mockOptions.Object);
        //
        //    //Act
        //    //await service.RegisterUser(
        //    //    new SignUpModel() 
        //    //    { FirstName = "test",
        //    //      Surname = "test",
        //    //      Email = "test@test.nl",
        //    //      Password = "test",
        //    //      Username = "test"
        //    //    });
        //    //
        //    //Assert
        //    //mockManager.Verify(x => x.CreateAsync(user, "test"), Times.Once);
        //    //mockManager.Verify(x => x.UpdateSecurityStampAsync(user), Times.Once);
        //    //mockManager.Verify(x => x.AddToRoleAsync(user, "User"), Times.Once);
        //}
        //
        //[Theory]
        //[InlineData("", "")]
        //[InlineData(null, null)]
        //public async void Should_ThrowException_When_NameIsEmptyOrNull(string firstname, string lastname)
        //{
        //    //Arrange
        //    var options = new JwtSettings()
        //    {
        //        Issuer = "issuer",
        //        Secret = "secret",
        //        ExpirationInMinutes = 60
        //    };
        //    var mockOptions = new Mock<IOptionsSnapshot<JwtSettings>>();
        //    mockOptions.Setup(x => x.Value).Returns(options);
        //    var context = new Mock<DataContext>();
        //
        //    var mockManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
        //
        //    var service = new UserService(mockManager.Object, mockOptions.Object);
        //
        //    //Act
        //    //await service.RegisterUser(
        //    //    new SignUpModel()
        //    //    {
        //    //        FirstName = firstname,
        //    //        Surname = lastname,
        //    //        Email = "test@test.nl",
        //    //        Password = "test",
        //    //        Username = "test"
        //    //    });
        //    //
        //    //Assert
        //    //mockManager.Verify(x => x.CreateAsync(user, "test"), Times.Once);
        //    //mockManager.Verify(x => x.UpdateSecurityStampAsync(user), Times.Once);
        //    //mockManager.Verify(x => x.AddToRoleAsync(user, "User"), Times.Once);
        //}
        //
        //[Theory]
        //[InlineData("")]
        //[InlineData(null)]
        //public async void Should_ThrowException_When_UserNameIsEmptyOrNull(string username)
        //{
        //    //Arrange
        //    var options = new JwtSettings()
        //    {
        //        Issuer = "issuer",
        //        Secret = "secret",
        //        ExpirationInMinutes = 60
        //    };
        //    var mockOptions = new Mock<IOptionsSnapshot<JwtSettings>>();
        //    mockOptions.Setup(x => x.Value).Returns(options);
        //    var context = new Mock<DataContext>();
        //
        //    var mockManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
        //
        //    var service = new UserService(mockManager.Object, mockOptions.Object);
        //
        //    //Act
        //    //await service.RegisterUser(
        //    //    new SignUpModel()
        //    //    {
        //    //        FirstName = "test",
        //    //        Surname = "test",
        //    //        Email = "test@test.nl",
        //    //        Password = "test",
        //    //        Username = username
        //    //    });
        //    //
        //    //Assert
        //    //mockManager.Verify(x => x.CreateAsync(user, "test"), Times.Once);
        //    //mockManager.Verify(x => x.UpdateSecurityStampAsync(user), Times.Once);
        //    //mockManager.Verify(x => x.AddToRoleAsync(user, "User"), Times.Once);
        //}
        //
        //[Theory]
        //[InlineData("")]
        //[InlineData(null)]
        //public void Should_ThrowException_When_EmailAdressIsEmptyOrNull(string email)
        //{
        //    //Arrange
        //
        //
        //    //Act
        //
        //
        //    //Assert
        //
        //}
        //
        //[Fact]
        //public void Should_ThrowException_When_UsernameUserIsNotUnique()
        //{
        //    //Arrange
        //
        //
        //    //Act
        //
        //
        //    //Assert
        //
        //}
        //
        //[Fact]
        //public void Should_ThrowException_When_EmailadressUserIsNotUnique()
        //{
        //    //Arrange
        //
        //
        //    //Act
        //
        //
        //    //Assert
        //    
        //}
    }
}
