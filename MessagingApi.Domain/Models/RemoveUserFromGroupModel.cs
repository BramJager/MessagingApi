namespace MessagingApi.Domain.Models
{
    public class RemoveUserFromGroupModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
