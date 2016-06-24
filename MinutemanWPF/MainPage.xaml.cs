using System;
using System.Collections.Generic;
using Minuteman.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.ProjectOxford.SpeechRecognition;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minuteman
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<Minute> Minutes
        {
            get { return (ObservableCollection<Minute>)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minutes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(ObservableCollection<Minute>), typeof(MainPage), new PropertyMetadata(null));

        public SpeechAPI.SpeechData<Minute> data;

        public async void RemoveSplash()
        {

            await Task.Delay(5000);
            var sb = Resources["GetRidOfSplash"] as Storyboard;
            sb.Begin();
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            var sb = Resources["RotateImage"] as Storyboard;
            sb.Begin();
            RemoveSplash();

            Minutes = new ObservableCollection<Minute>();

            SpeechAPI.SpeechAPI api = new SpeechAPI.SpeechAPI("d409ad7533194c30b1a9bb07bd43e386",
                "f525e0d0-51c1-4c47-9fe9-bb5b8b9cbdb5",
                "d4056ba9897b40c1bbdb0b1141a1aefd");

            SpeechAPI.SpeechIntent intent = new SpeechAPI.SpeechIntent("https://api.projectoxford.ai/luis/v1/application?id=f525e0d0-51c1-4c47-9fe9-bb5b8b9cbdb5&subscription-key=d4056ba9897b40c1bbdb0b1141a1aefd&q=");
            
            data = new SpeechAPI.SpeechData<Minute>(api, intent, Minutes, this.AddMinute, this.ModMinute);
            
        }

        public void AddMinute()
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                var min = new Minute();
                Minutes.Insert(0, min);
            }));
        }


        public void ModMinute(Minute min, string s)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                min.Parse(s);
            }));
        }


        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Array values = Enum.GetValues(typeof(MinuteType));
            Random random = new Random();
            MinuteType randomType = (MinuteType)values.GetValue(random.Next(values.Length));

            /*AddMinute(new Minute
            {
                Type = randomType,
                Timestamp = DateTime.Now,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
            });*/

        }
    }
}
