using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.ProjectOxford.SpeechRecognition;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace SpeechAPI
{
    public class SpeechAPI : IDisposable
    {
        private string _subscriptionKey;
        private string _luisAPI;
        private string _luisSub;

        public delegate void MicrophoneEventDelegate(object sender, MicrophoneEventArgs e);
        public event MicrophoneEventDelegate OnMicrophoneStatusEvent;

        public delegate void MicDictationReceivedEventDelegate(object sender, SpeechResponseEventArgs e);
        public event MicDictationReceivedEventDelegate OnResponseReceivedEvent;

        public delegate void MicDictationPartialReceivedEventDelegate(object sender, PartialSpeechResponseEventArgs e);
        public event MicDictationPartialReceivedEventDelegate OnPartialResponseReceivedEvent;
        
        public delegate void MicDictationErrorReceivedEventDelegate(object sender, SpeechErrorEventArgs e);
        public event MicDictationErrorReceivedEventDelegate OnConversationErrorEvent;
        

        /// <summary>
        /// The microphone client
        /// </summary>
        private MicrophoneRecognitionClient micClient;
        
        
        /// <summary>
        /// Gets the default locale.
        /// </summary>
        /// <value>
        /// The default locale.
        /// </value>
        private string DefaultLocale
        {
            get { return "en-US"; }
        }

        public SpeechAPI(string subscriptionKey, string luisAPI, string luisSub)
        {
            this._subscriptionKey = subscriptionKey;
            this._luisAPI = luisAPI;
            this._luisSub = luisSub;
            this.CreateMicrophoneRecoClient();
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != this.micClient)
                {
                    this.micClient.Dispose();
                }

            }
        }
        
        /// <summary>
        /// Creates a new microphone reco client without LUIS intent support.
        /// </summary>
        private void CreateMicrophoneRecoClient()
        {
            this.micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                SpeechRecognitionMode.LongDictation,
                this.DefaultLocale,
                this._subscriptionKey,
                this._subscriptionKey);

            // Event handlers for speech recognition results
            this.micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
            this.micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            this.micClient.OnResponseReceived += this.OnMicDictationResponseReceivedHandler;
            this.micClient.OnConversationError += this.OnConversationErrorHandler;
            this.micClient.StartMicAndRecognition();
        }
        

        /// <summary>
        /// Called when a final response is received;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnMicDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.EndOfDictation ||
                e.PhraseResponse.RecognitionStatus == RecognitionStatus.DictationEndSilenceTimeout)
            {
                this.CreateMicrophoneRecoClient();
            }

            if (OnResponseReceivedEvent != null)
                OnResponseReceivedEvent(sender, e);
            
        }


        /// <summary>
        /// Called when a partial response is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PartialSpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            if (OnPartialResponseReceivedEvent != null)
                OnPartialResponseReceivedEvent(sender, e);
        }

        /// <summary>
        /// Called when an error is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechErrorEventArgs"/> instance containing the event data.</param>
        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            if (OnConversationErrorEvent != null)
                OnConversationErrorEvent(sender, e);
        }

        /// <summary>
        /// Called when the microphone status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MicrophoneEventArgs"/> instance containing the event data.</param>
        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            if(OnMicrophoneStatusEvent != null)
                OnMicrophoneStatusEvent(sender, e);
        }
    }
}
