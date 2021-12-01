namespace MessagingApi.Domain.Objects
{
    public class GroupModel
    {
        public string Name { get; set; }
        public int MaxUsers { get; set; }
        public string Password { get; set; }
        public Visibility Visibility { get; set; }
    }
}
