using ContactForm.API.Data;

namespace ContactForm.API.Models
{
    public class Tenant : HelperFields
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ShortName { get; set; }
    }
}