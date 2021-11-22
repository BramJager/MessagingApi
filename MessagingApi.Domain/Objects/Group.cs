﻿using System.Collections.Generic;

namespace MessagingApi.Domain.Objects
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int MaxUsers { get; set; }
        public bool Private { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
