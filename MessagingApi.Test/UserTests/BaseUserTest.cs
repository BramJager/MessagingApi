using MessagingApi.Business;
using MessagingApi.Business.Settings;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace MessagingApi.Test.UserTests
{
    public class BaseUserTest
    {
        public readonly UserService service;
        public readonly JwtSettings options;
        public readonly Mock<IOptionsSnapshot<JwtSettings>> mockOptions;
        public readonly Mock<UserManager<User>> mockManager;

        public BaseUserTest()
        {
            options = new JwtSettings()
            {
                Issuer = "issuer",
                Secret = "secret",
                ExpirationInMinutes = 60
            };

            mockOptions = new Mock<IOptionsSnapshot<JwtSettings>>();
            mockManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            service = new UserService(mockManager.Object, mockOptions.Object);
        }
    }
}
