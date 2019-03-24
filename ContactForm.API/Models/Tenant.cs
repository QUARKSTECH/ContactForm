using System.Collections.Generic;
using ContactForm.API.Data;

namespace ContactForm.API.Models
{
    public class Tenant : HelperFields
    {
        public Tenant()
        {
            //Enquiries = new List<Enquiry>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string ShortName { get; set; }
        //public ICollection<Enquiry> Enquiries { get; set; }
    }
}