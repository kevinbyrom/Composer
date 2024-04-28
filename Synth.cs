using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Composer.Oscillators;
//using Composer.Effects;
//using Composer.Modifiers;
using Composer.Utilities;
using Composer.Nodes;
using Composer.Nodes.Sources;
using Composer.Nodes.Input;
using Composer.Nodes.Operators;
using Microsoft.Xna.Framework.Input;
using Composer.Nodes.Output;
using System.Diagnostics;
using System.IO;

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
        public double TimePerTick { get; private set; }
        
        public bool DebugMode { get; private set; }

        public IEnumerable<Signal> LastSignals { get { return lastSignals; } }

        //private Dictionary<int, Voice> keyVoices;
        private ISignalNode rootNode;
        private double currTime = 0.0;
        private Signal[] lastSignals;
        private StreamWriter debugWriter;

        public Synth(int sampleRate, ISignalTarget output, bool debugMode = false)
        {
            this.SampleRate = sampleRate;
            this.Output = output;
            this.TimePerTick = 1.0 / (double)sampleRate;
            this.DebugMode = debugMode;

            //this.keyVoices = new Dictionary<int, Voice>();

            var key1 = new KeyNode(Keys.A);
            var voice1 = new OscillatorNode(new SineWaveOscillator(Notes.E4), key1);

            //var constant2 = new OscillatorNode(new SineWaveOscillator(Notes.C4));
            //var voice2 = new KeyNode(Keys.S).Multiply(constant2);

            //var constant3 = new OscillatorNode(new SineWaveOscillator(Notes.G4));
            //var voice3 = new KeyNode(Keys.D).Multiply(constant3);

            //this.rootNode = new MixerNode(new ISignalNode[] { voice1, voice2, voice3 });
            this.rootNode = new MixerNode(new ISignalNode[] { voice1 });

            if (this.DebugMode)
                this.debugWriter = new StreamWriter("output.txt", false);
        }


        ~Synth()
        {
            if (this.DebugMode)
                this.debugWriter.Close();
        }

        public void SetupKey(int key, double freq)
        {
            //var voice = new Voice(new SineWaveOscillator(freq));

            //this.keyVoices.Add(key, voice);
        }


        public void KeyOn(int key)
        {

            // Get the voice associated with the key

            //var voice = this.keyVoices[key];


            // Turn the voice on

            //voice.On();

        }


        public void KeyOff(int key)
        {
            
            // Get the voice associated with they key

            //var voice = this.keyVoices[key];


            // Turn the voice off

            //voice.Off();

        }


        public void Update(double time)
        {

            // Determine how many ticks have elapsed since last time

            //int ticks = (int)(gameTime.ElapsedGameTime.TotalSeconds * this.SampleRate);

            //this.lastSignals = new Signal[ticks];

            // Get each voice signal for the sample and push to the output

            //for (int s = 0; s < ticks; s++)
            //{
                /*var voices = this.keyVoices.Values;

                var signals = new Signal[voices.Count];

                int i = 0;

                foreach (var voice in this.keyVoices.Values)
                {
                    voice.Update(this.currTime);
                    signals[i++] = voice.CurrSignal;
                }
                */

                this.rootNode.Update(time);
                var signal = this.rootNode.Signal;
                this.Output.Write(time, signal);
                //this.Output.Write(this.currTime, SignalMixer.Mix(signals));

                if (this.DebugMode)
                    this.debugWriter.WriteLine(signal.ToString());

                //this.currTime += this.TimePerTick;
                //this.lastSignals[s] = this.rootNode.Signal;
            //}
        }
    }
}
