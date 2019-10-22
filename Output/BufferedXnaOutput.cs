using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;


namespace Composer.Output
{
    public class BufferedXnaOutput : ISampleTarget
    {
        const int NumChannels = 2;
        const int BytesPerSample = 2;
        const int DefaultMaxSamples = 3000;

        private DynamicSoundEffectInstance instance;
        private List<Sample> samples;
        private byte[] xnaBuffer;


        public BufferedXnaOutput(DynamicSoundEffectInstance instance, int maxSamples = DefaultMaxSamples)
        {
            this.instance = instance;
            this.samples = new List<Sample>();
            this.xnaBuffer = new byte[NumChannels * maxSamples * BytesPerSample];
        }


        public void Write(Sample sample)
        {
            
            // Store the sample in the buffer

            //this.samples.Add(sample);
            
            
            // If we have filled the buffer, flush it to the XNA instance 

            //while (this.in)
            //if (this.count == maxSamples)
            //{
              //  Flush();
                //this.count = 0;
            //}
        }


        public void Write(IEnumerable<Sample> samples)
        {
            foreach (var sample in samples)
                Write(sample);
        }


        private void ConvertToXnaBuffer()
        {
            /*int pos = 0;

            for (int i = 0; i < this.count; i++)
            {
                Sample sample = this.samples[i];

                // Get short values for left & right
                
                short shortLeft = (short)(sample.Left >= 0.0f ? sample.Left * short.MaxValue : sample.Left * short.MinValue * -1);
                short shortRight = (short)(sample.Right >= 0.0f ? sample.Right * short.MaxValue : sample.Right * short.MinValue * -1);

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
            }*/
        }

        private void Flush()
        {
            ConvertToXnaBuffer();
            this.instance.SubmitBuffer(this.xnaBuffer);
        }
    }
}