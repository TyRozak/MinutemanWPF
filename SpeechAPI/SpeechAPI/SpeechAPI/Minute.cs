using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SpeechAPI
{
    public class Minute : IMinute
    {
        public enum MinuteType
        {
            Meeting,
            Action,
            Update
        }

        public MinuteType Type
        {
            get;
            set;
        }
        
        public string Description
        {
            get;
            set;
        }

        public DateTime Timestamp
        {
            get;
            set;
        }
        
        public Minute()
        {
            Timestamp = DateTime.Now;
        }
        
        public void Parse(string response)
        {
            Description = response;
            Type = MinuteType.Update;

            try
            {

                var jsonArticleFeed = JObject.Parse(response);
                if(jsonArticleFeed != null)
                {
                    if(jsonArticleFeed["query"] != null)
                        Description = jsonArticleFeed["query"].ToString();
                    var intents = jsonArticleFeed["intents"];
                    if(intents != null)
                    {
                        if (intents.Count() > 0)
                        {
                            var intent = intents[0];
                            if(intent != null)
                            {
                                if(intent["intent"] != null)
                                {
                                    var intentStr = intent["intent"].ToString();
                                    if(intentStr == "Meeting")
                                    {
                                        Type = MinuteType.Meeting;
                                    }
                                    else if(intentStr == "Update")
                                    {
                                        Type = MinuteType.Update;
                                    }
                                    else if(intentStr == "Action")
                                    {
                                        Type = MinuteType.Action;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                // Console.WriteLine(e);
            }
        }

        public override string ToString() {
            string description = "Description:" + Description + "\n"
                + "Type: " + Type.ToString(); 
            return description;
        }

    }
}
