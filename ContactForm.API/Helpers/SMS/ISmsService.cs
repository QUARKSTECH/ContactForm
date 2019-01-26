using System.Threading.Tasks;
using ContactForm.API.Dtos;

namespace ContactForm.API.Helpers.SMS
{
    public interface ISmsService
    {
         Task<string> postXMLData(string destinationUrl, string requestXml);
         Task<string> ParseXML(string xmlString);
         Task<string> ReadAndModifyXMLFile(EnquiryDto enquiryDto);
    }
}