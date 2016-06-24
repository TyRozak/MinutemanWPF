using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.SpeechRecognition;
using System.Collections.ObjectModel;

namespace SpeechAPI
{
    public class SpeechData<T> where T : IMinute, new()
    {

        public delegate void AddMinute();
        public delegate void ModMinute(T min, string s);

        SpeechAPI _api;
        SpeechIntent _intent;
        ObservableCollection<T> _minutes;
        AddMinute _addMinute;
        ModMinute _modMinute;

        public SpeechData(SpeechAPI api, SpeechIntent intent, ObservableCollection<T> minutes, AddMinute addNew, ModMinute modMin)
        {
            _api = api;
            _minutes = minutes;
            _intent = intent;
            //_addMinute = am;
            _modMinute = modMin;
            _api.OnPartialResponseReceivedEvent += OnPartialResponseReceivedHandler;
            _api.OnResponseReceivedEvent += OnMicDictationResponseReceivedHandler;
            _addMinute = addNew;
            if(_addMinute != null)
                _addMinute();
            //_minutes.Add(new T());

        }
        private void OnMicDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.Results.Length > 0)
            {
                var result = e.PhraseResponse.Results[0].DisplayText;
                var uri = result;

                //_minutes[_minutes.Count - 1].Parse(result);
                //RequestIntent(result, _minutes[_minutes.Count - 1]);


                if (_modMinute != null)
                    _modMinute(_minutes[0], result);
                RequestIntent(result, _minutes[0]);
            }
            

            //_minutes.Add(new T());

            if (_addMinute != null)
                _addMinute();
        }


        /// <summary>
        /// Called when a partial response is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PartialSpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            //_minutes[_minutes.Count - 1].Parse(e.PartialResult);
            if (_modMinute != null)
                _modMinute(_minutes[0], e.PartialResult);
        }

        public async void RequestIntent(string query, T minute)
        {
            Task<string> intentResponse = _intent.Request(query);
            string response = await intentResponse;
            if (_modMinute != null)
                _modMinute(minute, response);
            //minute.Parse(response);
        }
    }
}
