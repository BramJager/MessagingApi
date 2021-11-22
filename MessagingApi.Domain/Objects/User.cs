using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessagingApi.Domain.Objects
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
