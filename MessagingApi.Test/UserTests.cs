using MessagingApi.Domain.Objects;
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
        public void Should_ThrowSQLException_When_UsernameUserIsNotUnique()
        {
            //Arrange
            User user = new User() { Id = 1, UserName = "henkie"};

            //Act
            //Action action = () => RegisterUser(new User{ Id = 2, UserName = "henkie", Email = "test@test.nl", PasswordHash = "heo230htwvpfbaw"});

            //Assert
            //Assert.Throws<SqlException>(action);
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
