using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Composer.Oscillators;
using Composer.Effects;
using Composer.Modifiers;
using Composer.Utilities;


namespace Composer
{
    public class Synth 
    {
        public ISignalTarget Output { get; private set; }

        //public IOscillator Oscillator { get; set; }
        //public ISignalTarget Target { get; private set; }
        //public List<ISignalTransform> Filters { get; set; }
        //public List<ISignalTransform> Amplifiers { get; set; }
        //public List<ISignalTransform> PostEffects { get; set; }
        //public VoiceGroup Voices { get; private set; }

        public int SampleRate { get; private set; }
        public double TimePerFrame { get; private set; }

        private Dictionary<int, Voice> keyVoices;
        private double currTime = 0.0;


        public Synth(int sampleRate, ISignalTarget output)
        {
            this.SampleRate = sampleRate;
            this.Output = output;
            this.TimePerFrame = 1.0 / (double)sampleRate;
            this.keyVoices = new Dictionary<int, Voice>();
        }


        public void SetupKey(int key, double freq)
        {
            var voice = new Voice(new SineWaveOscillator(freq).Envelope());

            this.keyVoices.Add(key, voice);
        }


        public void KeyOn(int key)
        {

            // Get the voice associated with the key

            var voice = this.keyVoices[key];


            // Turn the voice on

            voice.On();

        }


        public void KeyOff(int key)
        {
            
            // Get the voice associated with they key

            var voice = this.keyVoices[key];


            // Turn the voice off

            voice.Off();

        }


        public void Update(GameTime gameTime)
        {

            // Determine how many samples have elapsed since last time

            int numSamples = (int)(gameTime.ElapsedGameTime.TotalSeconds * this.SampleRate);


            // Get each voice signal for the sample and push to the output

            for (int s = 0; s < numSamples; s++)
            {
                var voices = this.keyVoices.Values;

                var signals = new Signal[voices.Count];

                int i = 0;

                foreach (var voice in this.keyVoices.Values)
                {
                    voice.Update(this.currTime);
                    signals[i++] = voice.CurrSignal;
                }

                this.Output.Write(this.currTime, SignalMixer.Mix(signals));
                

                this.currTime += this.TimePerFrame;
            }
        }
    }
}
