using MessagingApi.Business;
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
        [Fact]
        public void Should_RegisterNewUser()
        {
            //Arrange


            //Act


            //Assert

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowException_When_NameIsEmptyOrNull(string name)
        {
            //Arrange


            //Act


            //Assert

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowException_When_UserNameIsEmptyOrNull(string username)
        {
            //Arrange


            //Act


            //Assert

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowException_When_EmailAdressIsEmptyOrNull(string email)
        {
            //Arrange


            //Act


            //Assert

        }

        [Fact]
        public void Should_ThrowException_When_UsernameUserIsNotUnique()
        {
            //Arrange


            //Act


            //Assert

        }

        [Fact]
        public void Should_ThrowException_When_EmailadressUserIsNotUnique()
        {
            //Arrange


            //Act


            //Assert
            
        }
    }
}
