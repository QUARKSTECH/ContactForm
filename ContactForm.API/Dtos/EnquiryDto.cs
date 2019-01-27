using System.Collections.Generic;

namespace ContactForm.API.Dtos
{
    public class EnquiryDto
    {
        public EnquiryDto()
        {
            ExtraProps = new Dictionary<string, object>();
            IsLogin = false;
        }
        public int Id { get; set; }
        public bool IsLogin { get; set; }
        public Dictionary<string, object> ExtraProps { get; set; }
    }
}