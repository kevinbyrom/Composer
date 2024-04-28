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

        public Signal LastSignal { get { return lastSignal; } }

        //private Dictionary<int, Voice> keyVoices;
        private ISignalNode rootNode;
        private double currTime = 0.0;
        private Signal lastSignal;
        private StreamWriter debugWriter;

        public Synth(int sampleRate, ISignalTarget output, bool debugMode = false)
        {
            this.SampleRate = sampleRate;
            this.Output = output;
            this.TimePerTick = 1.0 / (double)sampleRate;
            this.DebugMode = debugMode;

            //this.keyVoices = new Dictionary<int, Voice>();

            var key1 = new KeyNode(Keys.A);
            var voice1 = new OscillatorNode(new SquareWaveOscillator(Notes.E4), key1);

            var key2 = new KeyNode(Keys.S);
            var voice2 = new OscillatorNode(new SineWaveOscillator(Notes.C4), key2);

            var key3 = new KeyNode(Keys.D);
            var voice3 = new OscillatorNode(new NoiseOscillator(Notes.G4), key3);

            this.rootNode = new MixerNode(new ISignalNode[] { voice1, voice2, voice3 });

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
            this.rootNode.Update(time);
            this.Output.Write(time, this.rootNode.Signal);
            this.lastSignal = this.rootNode.Signal;

            if (this.DebugMode)
                this.debugWriter.WriteLine(this.lastSignal.ToString());

        }
    }
}
