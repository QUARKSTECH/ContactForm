using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;
using ContactForm.API.Dtos;

namespace ContactForm.API.Helpers.SMS
{
    public class SmsService : ISmsService
    {
        private const string URL = "http://smsp.myoperator.co/api/postsms.php";
        public async Task<string> ParseXML(string xmlString)
        {
            if (xmlString != null)
            {
                var test = xmlString.Split(new string[] { "spc" }, StringSplitOptions.None);
                var xmlData = "";
                for (int i = 0; i < test.Length; i++)
                {
                    if (i < test.Length - 1)
                        xmlData += test[i] + "%0a";
                    else
                        xmlData += test[i];
                }
                var data = await postXMLData(URL, xmlData);
                return data.ToString();
            }
            else
                return null;
        }

        public async Task<string> postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = await request.GetRequestStreamAsync();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        public async Task<string> ReadAndModifyXMLFile(EnquiryDto enquiryDto)
        {
            var extraProps = new Dictionary<string, object>();
            extraProps.Add("Name", enquiryDto.Name);
            extraProps.Add("Email", enquiryDto.Email);
            extraProps.Add("Mobile", enquiryDto.Mobile);
            extraProps.Add("Enquiry", enquiryDto.Message);
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/SmsTemplate", "sms.xml");
            var xmlPath = File.ReadAllText(path);
            foreach (KeyValuePair<string, object> item in extraProps)  
            {  
                xmlPath = xmlPath.Replace($"#{item.Key}#",item.Value.ToString());
            } 
            var sms = await ParseXML(xmlPath);
            return sms;
        }
    }
}