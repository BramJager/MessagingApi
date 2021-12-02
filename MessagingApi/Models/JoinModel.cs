namespace MessagingApi.Models
{
    public class JoinModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
    }
}
