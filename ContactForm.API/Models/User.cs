using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ContactForm.API.Models
{
    public class User : IdentityUser<int>
    {
        public string ContactNumber { get; set; }
        public string FullName { get; set; }
        public int OTP { get; set; }

        public DateTime CreatedDate { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}