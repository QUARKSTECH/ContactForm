using System;
using System.Collections.Generic;

namespace ContactForm.API.Dtos
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ShortName { get; set; }
        public Dictionary<string, object> ExtraProps { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}