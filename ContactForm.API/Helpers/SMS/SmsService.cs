using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;
using ContactForm.API.Dtos;
using Microsoft.Extensions.Options;

namespace ContactForm.API.Helpers.SMS
{
    public class SmsService : ISmsService
    {
        private readonly IOptions<SmsConfigurations> _smsConfigurations;
        private Dictionary<string, object> smsConfProps;
        private string xmlString;
        public SmsService(IOptions<SmsConfigurations> smsConfigurations)
        {
            _smsConfigurations = smsConfigurations;
            SetSMSConfigurations();
        }
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
                var data = await postXMLData(_smsConfigurations.Value.POSTSMSAPI, xmlData);
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
            string textContent = "";
            if (enquiryDto.IsLogin)
            {
                xmlString = xmlString.Replace("textContent", _smsConfigurations.Value.LOGINTEXT);
                xmlString = xmlString.Replace("ADDRESS", _smsConfigurations.Value.LOGINADDRESS);
                xmlString = xmlString.Replace("#otp#", enquiryDto.ExtraProps["otp"].ToString());
            }
            else
            {
                foreach (KeyValuePair<string, object> item in enquiryDto.ExtraProps)
                {
                    textContent += $"{item.Key }: {item.Value.ToString()}spc";
                }
                xmlString = xmlString.Replace("textContent", textContent);
                xmlString = xmlString.Replace("ADDRESS", _smsConfigurations.Value.ENQUIRYADDRESS);
            }
            xmlString = xmlString.Replace("#Mobile#", enquiryDto.ExtraProps["Mobile"].ToString());

            var sms = await ParseXML(xmlString);
            return sms;
        }

        private void SetSMSConfigurations()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/SmsTemplate", "sms.xml");
            xmlString = File.ReadAllText(path);
            smsConfProps = new Dictionary<string, object>()
            {
                {"AUTHKEY", _smsConfigurations.Value.AUTHKEY},
                {"SENDER", _smsConfigurations.Value.SENDER},
                {"ROUTE", _smsConfigurations.Value.ROUTE},
                {"CAMPAIGN", _smsConfigurations.Value.CAMPAIGN},
                {"COUNTRY", _smsConfigurations.Value.COUNTRY}
            };

            foreach (KeyValuePair<string, object> item in smsConfProps)
            {
                xmlString = xmlString.Replace($"#{item.Key}#", item.Value.ToString());
            }
        }
    }
}