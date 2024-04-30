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
using Composer.Nodes.Modifiers;
using Composer.Nodes.Effects;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Transactions;

namespace Composer
{
    public class Synth 
    {
        public ISignalTarget Output { get; private set; }
        public int SampleRate { get; private set; }
        public double TimePerTick { get; private set; }
        
        public bool DebugMode { get; private set; }

        public Signal LastSignal { get { return lastSignal; } }

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

            ISignalNode[] voices = new ISignalNode[12];

            voices[0] = CreateVoice(Notes.HZ(Notes.Key.C), Keys.A);
            voices[1] = CreateVoice(Notes.HZ(Notes.Key.CS), Keys.W);
            voices[2] = CreateVoice(Notes.HZ(Notes.Key.D), Keys.S);
            voices[3] = CreateVoice(Notes.HZ(Notes.Key.DS), Keys.E);
            voices[4] = CreateVoice(Notes.HZ(Notes.Key.E), Keys.D);
            voices[5] = CreateVoice(Notes.HZ(Notes.Key.F), Keys.F);
            voices[6] = CreateVoice(Notes.HZ(Notes.Key.FS), Keys.R);
            voices[7] = CreateVoice(Notes.HZ(Notes.Key.G), Keys.G);
            voices[8] = CreateVoice(Notes.HZ(Notes.Key.GS), Keys.T);
            voices[9] = CreateVoice(Notes.HZ(Notes.Key.A), Keys.H);
            voices[10] = CreateVoice(Notes.HZ(Notes.Key.AS), Keys.Y);
            voices[11] = CreateVoice(Notes.HZ(Notes.Key.B), Keys.J);

            this.rootNode = new MixerNode(voices);//.Reverb(.278); //.Delay(1, .5);

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
            this.currTime = time;
            this.rootNode.Update(time);
            this.Output.Write(time, this.rootNode.Signal);
            this.lastSignal = this.rootNode.Signal;

            if (this.DebugMode)
                this.debugWriter.WriteLine(this.lastSignal.ToString());

        }

        private ISignalNode CreateVoice(double freq, Microsoft.Xna.Framework.Input.Keys inputKey)
        {
            var key = new KeyNode(inputKey);
            var env = new EnvelopeNode(key);
            env.Settings.Attack.Level = () => { return 1; };
            env.Settings.Attack.Duration = () => { return 0.05; };
            env.Settings.Decay.Level = () => { return 0.75; };
            env.Settings.Decay.Duration = () => { return 0.05; };
            env.Settings.Sustain.Level = () => { return 0.75; };
            env.Settings.Sustain.Duration = () => { return 0.1; };
            env.Settings.Sustain.Latch = () => { return key.Signal.IsActive; };
            env.Settings.Release.Level = () => { return 0; };
            env.Settings.Release.Duration = () => { return 1; };

            

            var pred = new PredicatedConstantNode(Signal.Max * .01, () => { return env.Signal.IsActive; });
            var osc = new SineWaveOscillator(freq);
            var freqOsc = new SineWaveOscillator(.2);
            var ampOsc = new SineWaveOscillator(4);
            var voice = new OscillatorNode(osc, pred);
            //osc.Frequency = () => { return freq - (freqOsc.GetValue(this.currTime).Value * 10); };
            //voice.Amp = () => { return ampOsc.GetValue(this.currTime).Value; };

            return voice.Multiply(env);//.Compress(-0.5, 0.5).Noise(0.0125);
        }
    }
}
