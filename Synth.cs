using System;
using System.Collections.Generic;
using Composer.Oscillators;
using Composer.Effects;


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
            this.PostEffects = new List<ISampleTransform>();
            this.Voices = new VoiceGroup();
            this.noteRegistry = new Dictionary<int, Action>();
        }



        public Voice PlayNote(int note, double duration = -1)
        {

            // Only play the note if not already playing

            if (this.noteRegistry.ContainsKey(note))
                return null;


            // Construct the signal function

            Func<SampleTime, Sample> sampleFunc = (time) =>
            {
                var osc = new SineWaveOscillator(NoteToFrequency(note));
                
                // Get initial signal from oscillator

                Sample sample = osc.GetValue(time.Current);

                // Apply transform functions

                this.Filters.ForEach((transform) => { sample = transform.Transform(time, sample); });
                this.Amplifiers.ForEach((transform) => { sample = transform.Transform(time, sample); });
                this.PostEffects.ForEach((transform) => { sample = transform.Transform(time, sample); });
                
                return sample;
            };

            // Set up the sample functions (construct with mods)

            //Func<SampleTime, Sample> signalFunc = (time) => { return this.Oscillator.GetValue(time.Current, NoteToFrequency(note)); };
            //Func<SampleTime, Sample> ampFunc = (time) => { return new Sample(1); };
            
            
            // Create a new voice 

            Voice voice = new Voice(sampleFunc, duration, this.Target);
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
            // Mix the voices and send through global modifiers
            // Send modified sample to target

            this.Voices.Update(time);
        }


        private float NoteToFrequency(int note)
        {
            return (float)(440.0 * Math.Pow(2, (note - 9) / 12.0f));
        }
    }
}
