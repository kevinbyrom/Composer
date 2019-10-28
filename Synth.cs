using System;
using System.Collections.Generic;
using Composer.Signals;
using Composer.Effects;


namespace Composer
{
    public class Synth 
    {
        public ISignalSource Oscillator { get; set; }

        private VoiceGroup voices;
        private readonly Dictionary<int, Action> noteRegistry;


        public Synth(VoiceGroup voices)
        {
            this.voices = voices;
            this.noteRegistry = new Dictionary<int, Action>();
        }


        public void PlayNote(int note, double duration)
        {
            
        }


        public void NoteOn(int note)
        {

            // Only play the note if not already playing

            if (this.noteRegistry.ContainsKey(note))
                return;


            // Find a free voice to use

            Voice voice = new Voice(this.Oscillator, NoteToFrequency(note), 1, -1);

            this.noteRegistry[note] = () => { voice.Release(); };

            this.voices.Add(voice);
        }


        public void NoteOff(int note)
        {
            if (this.noteRegistry.ContainsKey(note))
            {
                this.noteRegistry[note]();
                this.noteRegistry.Remove(note);
            }
        }
        


        private float NoteToFrequency(int note)
        {
            return (float)(440.0 * Math.Pow(2, (note - 9) / 12.0f));
        }
    }
}
