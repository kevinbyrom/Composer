using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;


namespace Composer.Output
{
    public class BufferedXnaOutput : ISignalTarget
    {
        const int NumChannels = 2;
        const int BytesPerSignal = 2;
        const int DefaultMaxSignals = 3000;

        private DynamicSoundEffectInstance instance;
        private List<Signal> signals;
        private int maxSignals;
        private byte[] xnaBuffer;

        public BufferedXnaOutput(DynamicSoundEffectInstance instance, int maxSignals = DefaultMaxSignals)
        {
            this.instance = instance;
            this.signals = new List<Signal>();
            this.maxSignals = maxSignals;
            this.xnaBuffer = new byte[NumChannels * maxSignals * BytesPerSignal];
        }


        public void Write(SampleTime time, Signal signal)
        {
            
            // Store the signal in the buffer

            this.signals.Add(signal);
            
            
            // If we have filled the buffer, flush it to the XNA instance 

            if (this.signals.Count == maxSignals)
            {
                Flush(); 
            }
        }


        public void Write(SampleTime time, IEnumerable<Signal> signals)
        {
            foreach (var signal in signals)
                Write(time, signal);
        }


        private void ConvertToXnaBuffer()
        {
            int pos = 0;

            for (int i = 0; i < this.signals.Count; i++)
            {
                Signal s = this.signals[i];

                // Get short values for left & right
                
                short shortLeft = (short)(s.Value >= 0.0f ? s.Value * short.MaxValue : s.Value * short.MinValue * -1);
                short shortRight = (short)(s.Value >= 0.0f ? s.Value * short.MaxValue : s.Value * short.MinValue * -1);

                // Store as 16 bit sample
                
                if (!BitConverter.IsLittleEndian)
                {
                    this.xnaBuffer[pos++] = (byte)(shortLeft >> 8);
                    this.xnaBuffer[pos++] = (byte)shortLeft;
                    this.xnaBuffer[pos++] = (byte)(shortRight >> 8);
                    this.xnaBuffer[pos++] = (byte)shortRight;
                }
                else
                {
                    this.xnaBuffer[pos++] = (byte)shortLeft;
                    this.xnaBuffer[pos++] = (byte)(shortLeft >> 8);
                    this.xnaBuffer[pos++] = (byte)shortRight;
                    this.xnaBuffer[pos++] = (byte)(shortRight >> 8);
                }
            }
        }

        public void Flush()
        {
            ConvertToXnaBuffer();
            this.instance.SubmitBuffer(this.xnaBuffer);
            this.signals.Clear();
        }
    }
}
