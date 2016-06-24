using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.SpeechRecognition;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SpeechAPI
{
    public class SpeechIntent
    {
        public delegate void OnRequestReceived(string resp);
        string _endPoint;

        public SpeechIntent(string endPoint)
        {
            _endPoint = endPoint;
        }

        public async Task<string> Request(string query)
        {
            string jsonString = "";
            var uriString = _endPoint + query;
            try
            {
                using (var webClient = new HttpClient())
                {
                    var response = await webClient.GetAsync(new Uri(uriString)).ConfigureAwait(false);
                    jsonString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Article Service Not Working - Network");
            }
            return jsonString;
        }
        
    }
}
