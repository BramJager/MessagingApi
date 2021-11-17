using MessagingApi.Business.Objects;
using System;
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

        [Fact]
        public void Should_ThrowException_When_NameIsEmptyOrNull()
        {
            //Arrange

            //Act

            //Assert

        }

        [Fact]
        public void Should_ThrowException_When_UserNameIsEmptyOrNull()
        {
            //Arrange

            //Act

            //Assert

        }

        [Fact]
        public void Should_ThrowException_When_EmailAdressIsEmptyOrNull()
        {
            //Arrange

            //Act

            //Assert

        }

        [Fact]
        public void Should_ThrowException_When_UsernameUserIsNotUnique()
        {
            //Arrange
            User user = new User() { Id = 1, UserName = "henkie"};

            //Act
            Action action = () => user.Id.ToString();


            //Assert
            Assert.Throws<ArgumentException>(action);
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
