using MessagingApi.Business;
using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Moq;

namespace MessagingApi.Test.GroupServiceTests
{
    public class BaseGroupTest
    {
        public readonly GroupService service;
        public readonly Mock<IRepository<Group>> mockRepository;

        public BaseGroupTest()
        {
            mockRepository = new Mock<IRepository<Group>>();
            service = new GroupService(mockRepository.Object);
        }
    }
}
