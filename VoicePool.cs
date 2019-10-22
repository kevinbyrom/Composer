using System;
using System.Collections.Generic;
using System.Linq;
using Composer.Signals;


namespace Composer
{
    public class VoiceGroup 
    {
        private readonly List<Voice> voices;

        public IEnumerable<Voice> Voices
        {
            get
            {
                return this.voices.ToArray();
            }
        }

        public IEnumerable<Sample> Samples
        {
            get
            {
                return this.voices.Select(v => v.CurrSample).ToArray();
            }
        }

        public VoiceGroup()
        {
            this.voices = new List<Voice>();
        }

        public void Update()
        {

            // Update the active voices

            foreach (var voice in this.voices)
            {
                voice.Update();
            }


            // Remove the inactive

            ReleaseInactive();
        }


        public void Add(Voice voice)
        {
            this.voices.Add(voice);
        }


        public void ReleaseInactive()
        {
            List<Voice> inactive = new List<Voice>();

            foreach (var voice in this.voices.Select(v => v).Where(v => !v.IsActive))
            {
                inactive.Add(voice);
            }

            foreach (var voice in inactive)
            {
                this.voices.Remove(voice);
            }
        }
    }
}
