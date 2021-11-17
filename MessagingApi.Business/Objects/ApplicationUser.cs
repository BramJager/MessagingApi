using Microsoft.AspNetCore.Identity;
using System;

namespace MessagingApi.Business.Objects
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = $"{FirstName} {Surname}";
            }
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
