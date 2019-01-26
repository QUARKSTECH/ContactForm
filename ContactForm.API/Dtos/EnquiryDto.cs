using System.Collections.Generic;

namespace ContactForm.API.Dtos
{
    public class EnquiryDto
    {
        public EnquiryDto()
        {
            ExtraProps = new Dictionary<string, object>();
        }
        public int Id { get; set; }
        public Dictionary<string, object> ExtraProps { get; set; }
    }
}