using ContactForm.API.Data;

namespace ContactForm.API.Models
{
    public class Enquiry : HelperFields
    {
        public int Id { get; set; }
        //public virtual Tenant Tenant { get; set; }
        public int TenantId { get; set; }
    }
}