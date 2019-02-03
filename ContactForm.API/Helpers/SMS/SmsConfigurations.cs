using System.Collections.Generic;

namespace ContactForm.API.Helpers.SMS
{
    public class SmsConfigurations
    {
        public string AUTHKEY { get; set; }
        public string SENDER { get; set; }
        public int ROUTE { get; set; }
        public string CAMPAIGN { get; set; }
        public int COUNTRY { get; set; }
        public string ENQUIRYADDRESS { get; set; }
        public string LOGINADDRESS { get; set; }
        public string LOGINTEXT { get; set; }
        public string POSTSMSAPI { get; set; }

    }
}