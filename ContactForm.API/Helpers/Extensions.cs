using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ContactForm.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static Dictionary<string, object> DeserializeDictionary(this string input)
        {
            return input.DeserializeDictionary<string, object>();
        }

        public static Dictionary<TKey, Tvalue> DeserializeDictionary<TKey, Tvalue>(this string input)
        {
            if(input != string.Empty)
            {
                return JsonConvert.DeserializeObject<Dictionary<TKey, Tvalue>>(input);
            }
            return new Dictionary<TKey, Tvalue>();
        }
    }
}