using System;
using System.Collections.Generic;
using Composer.Oscillators;
using Composer.Effects;
using Composer.Modifiers;


namespace Composer
{
    public class Synth 
    {
        public ISampleTarget Target { get; private set; }
        public List<ISampleTransform> Filters { get; set; }
        public List<ISampleTransform> Amplifiers { get; set; }
        public List<ISampleTransform> PostEffects { get; set; }
        public VoiceGroup Voices { get; private set; }

        private readonly Dictionary<int, Action> noteRegistry;


        public Synth(ISampleTarget target)
        {
            this.Target = target;

            this.Filters = new List<ISampleTransform>();

            this.Amplifiers = new List<ISampleTransform>();
            this.Amplifiers.Add(new AmpEnvelopeModifier());

            this.PostEffects = new List<ISampleTransform>();

            this.Voices = new VoiceGroup();
            this.noteRegistry = new Dictionary<int, Action>();
        }



        public Voice PlayNote(int note, double duration = -1)
        {

            // Only play the note if not already playing

            if (this.noteRegistry.ContainsKey(note))
                return null;


            // Create the new voice

            var voice = SpawnVoice(note, duration);

            this.Voices.Add(voice);


            // Track the note so we can release it later

            this.noteRegistry[note] = () => { voice.Release(); };

            return voice;
        }


        public void NoteOff(int note)
        {
            if (this.noteRegistry.ContainsKey(note))
            {
                this.noteRegistry[note]();
                this.noteRegistry.Remove(note);
            }
        }
        


        public void Update(SampleTime time)
        {
            
            // Update the voices
            
            this.Voices.Update(time);

            // Mix the voices and send through global modifiers
            // Send modified sample to target

            
        }


        private Voice SpawnVoice(int note, double duration)
        {

            // Setup the oscillator

            var osc = new SineWaveOscillator(NoteToFrequency(note));
                

            // Setup the transforms

            var transforms = new List<ISampleTransform>();

            transforms.Add(new AmpEnvelopeModifier());


            // Create the voice

            Voice voice = new Voice(osc, transforms, this.Target, duration);

            return voice;
        }

        private float NoteToFrequency(int note)
        {
            return (float)(440.0 * Math.Pow(2, (note - 9) / 12.0f));
        }
    }
}
