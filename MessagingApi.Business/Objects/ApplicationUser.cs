using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessagingApi.Business.Objects
{
    public class ApplicationUser : IdentityUser<int>
    {
        private string _name;

        [Required]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = $"{FirstName} {Surname}";
            }
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
