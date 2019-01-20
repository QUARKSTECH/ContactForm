using System.Collections.Generic;

namespace ContactForm.API.Helpers.Mail
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
	    List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}