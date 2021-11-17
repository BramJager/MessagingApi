using System.Collections.Generic;

namespace MessagingApi.Business.Objects
{
    public class Group
    {
        public int GroupId { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
