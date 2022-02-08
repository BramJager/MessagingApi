using MessagingApi.Domain.Objects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagingApi.Test.GroupServiceTests
{
    public class GetGroupTest : BaseGroupTest
    {
        public GetGroupTest() : base()
        {

        }

        [Fact]
        public async void Should_GetUserById()
        {
            //Arrange
            int id = 1;
            mockRepository.Setup(x => x.GetById(id)).Returns(Task.FromResult(new Group() { GroupId = id}));

            //Act
            var group = await service.GetGroupById(id);

            //Assert
            mockRepository.Verify(x => x.GetById(id), Times.Once);
            Assert.Equal(id, group.GroupId);
        }
    }
}
