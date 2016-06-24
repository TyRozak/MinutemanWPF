using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace Minuteman.Models
{
    public enum MinuteType {
        Meeting,
        Action,
        Update
    }
    public class Minute : DependencyObject, SpeechAPI.IMinute
    {
        public MinuteType Type
        {

            get {
                return (MinuteType)GetValue(TypeProperty); }
            set {
                if (value == MinuteType.Action)
                {
                    ActionImageURL =  new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + "//Assets//Icons//app_actionicon.png"));
                    ActionMetadata = "Action";
                }
                else if (value == MinuteType.Meeting)
                {
                    ActionImageURL = new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + "//Assets//Icons//app_meetingicon.png"));
                    ActionMetadata = "Meeting";
                }
                else
                {
                    ActionImageURL = new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + "//Assets//Icons//app_updateicon.png"));
                    ActionMetadata = "Update";
                }
                SetValue(TypeProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(MinuteType), typeof(Minute), new PropertyMetadata(MinuteType.Update));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(Minute), new PropertyMetadata(""));

        public DateTime Timestamp
        {
            get { return (DateTime)GetValue(TimestampProperty); }
            set { SetValue(TimestampProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Timestamp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimestampProperty =
            DependencyProperty.Register("Timestamp", typeof(DateTime), typeof(Minute), new PropertyMetadata(null));


        public ImageSource ProfileImageURI
        {
            get {

                var names = new List<string> { "//Assets//Profiles//app_profile1.png",
                    "//Assets//Profiles//app_profile2.png", "//Assets//Profiles//app_profile3.png"};
                Random r = new Random();
                int index = r.Next(names.Count);
                var name = names[index];

                return new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + name));
            }
        }

       
        public string ProfileName
        {
            get {
                var names = new List<string> { "Alex Pezzi", "Kirisanth S.", "Kalon Winnik", "Jorge Mijangos", "Ty Rozak" };
                Random r = new Random();
                int index = r.Next(names.Count);
                var name = names[index];
                return name;
            }
        }

        public ImageSource ActionImageURL
        {
            get { return (ImageSource)GetValue(ActionImageURLProperty); }
            set { SetValue(ActionImageURLProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionImageURL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionImageURLProperty =
            DependencyProperty.Register("ActionImageURL", typeof(ImageSource), typeof(Minute), new PropertyMetadata(new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + "//Assets//Icons//app_updateicon.png"))));



        //public ImageSource ActionImageURL
        //{
        //    get {
        //        if (Type == MinuteType.Action)
        //        {
        //            return new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" + "//Assets//Icons//app_actionicon.png"));
        //        }
        //        else if (Type == MinuteType.Meeting)
        //        {
        //            return new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF" +"//Assets//Icons//app_meetingicon.png"));
        //        }
        //        else 
        //        {
        //            return new BitmapImage(new Uri("C:\\Users\\TyRozak\\Documents\\Visual Studio 2015\\Projects\\MinutemanWPF\\MinutemanWPF"+ "//Assets//Icons//app_updateicon.png"));
        //        }
        //    }
           
        //}
        
        public string ActionMetadata
        {
            get { return (string)GetValue(ActionMetadataProperty); }
            set { SetValue(ActionMetadataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionMetadata.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionMetadataProperty =
            DependencyProperty.Register("ActionMetadata", typeof(string), typeof(Minute), new PropertyMetadata("Metadata"));



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
                if (jsonArticleFeed != null)
                {
                    if (jsonArticleFeed["query"] != null)
                        Description = jsonArticleFeed["query"].ToString();
                    var intents = jsonArticleFeed["intents"];
                    if (intents != null)
                    {
                        if (intents.Count() > 0)
                        {
                            var intent = intents[0];
                            if (intent != null)
                            {
                                if (intent["intent"] != null)
                                {
                                    var intentStr = intent["intent"].ToString();
                                    if (intentStr == "Meeting")
                                    {
                                        Type = MinuteType.Meeting;
                                    }
                                    else if (intentStr == "Update")
                                    {
                                        Type = MinuteType.Update;
                                    }
                                    else if (intentStr == "Action")
                                    {
                                        Type = MinuteType.Action;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
            }
        }

    }
}
