using System.Collections.Generic;

namespace MessagingApi.Domain.Objects
{
    public class Group
    {
        public int GroupId { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
