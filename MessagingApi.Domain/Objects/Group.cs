using System.Collections.Generic;

namespace MessagingApi.Domain.Objects
{
    public enum Visibility
    {
        Public,
        Private
    }

    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int MaxUsers { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool Removed { get; set; }
        public Visibility Visibility { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
