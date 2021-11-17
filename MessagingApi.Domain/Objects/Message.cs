using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingApi.Domain.Objects
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Content { get; set; }

        private DateTime _created;
        public DateTime DateTime
        {
            get { return _created; }
            set
            {
                _created = DateTime.Now;
            }
        }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
