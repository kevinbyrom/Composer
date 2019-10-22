using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Composer.Output;


namespace Composer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int SampleRate = 44100;
        //private DynamicSoundEffectInstance instance;
        private VoiceGroup voices;
        private Synth synth;
        private ISampleTarget output;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            // Setup the output

            var instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
            instance.Play();

            var xnaOutput = new BufferedXnaOutput(instance);
            this.output = new Mixer(xnaOutput);


            // Setup the synth

            this.voices = new VoiceGroup();
            this.synth = new Synth(this.voices);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { this.synth.NoteOn(0); } else { this.synth.NoteOff(0); }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { this.synth.NoteOn(2); } else { this.synth.NoteOff(2); }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { this.synth.NoteOn(4); } else { this.synth.NoteOff(4); }
            if (Keyboard.GetState().IsKeyDown(Keys.F)) { this.synth.NoteOn(5); } else { this.synth.NoteOff(5); }
            if (Keyboard.GetState().IsKeyDown(Keys.G)) { this.synth.NoteOn(7); } else { this.synth.NoteOff(7); }

            voices.Update();
            this.output.Write(voices.Samples);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        /*private static void ConvertBuffer(float[,] from, byte[] to)
        {
            const int bytesPerSample = 2;
            int channels = from.GetLength(0);
            int samplesPerBuffer = from.GetLength(1);

            // Make sure the buffer sizes are correct
            System.Diagnostics.Debug.Assert(to.Length == samplesPerBuffer * channels * bytesPerSample, "Buffer sizes are mismatched.");

            for (int i = 0; i < samplesPerBuffer; i++)
            {
                for (int c = 0; c < channels; c++)
                {
                    // First clamp the value to the [-1.0..1.0] range
                    float floatSample = MathHelper.Clamp(from[c,i], -1.0f, 1.0f);

                    // Convert it to the 16 bit [short.MinValue..short.MaxValue] range
                    short shortSample = (short)(floatSample >= 0.0f ? floatSample * short.MaxValue : floatSample * short.MinValue * -1);

                    // Calculate the right index based on the PCM format of interleaved samples per channel [L-R-L-R]
                    int index = i * channels * bytesPerSample + c * bytesPerSample;

                    // Store the 16 bit sample as two consecutive 8 bit values in the buffer with regard to endian-ness
                    if (!BitConverter.IsLittleEndian)
                    {
                        to[index] = (byte)(shortSample >> 8);
                        to[index + 1] = (byte)shortSample;
                    }
                    else
                    {
                        to[index] = (byte)shortSample;
                        to[index + 1] = (byte)(shortSample >> 8);
                    }
                }
            }
        }*/


        /*private void FillWorkingBuffer()
        {
            for (int i = 0; i < SamplesPerBuffer; i++)
            {
                // Here is where you sample your wave function

                var val = this.synth.NextSample();

                this.workingBuffer[0, i] = val.Left;
                this.workingBuffer[1, i] = val.Right;
            }
        }*/

        /*private void SubmitBuffer()
        {
            FillWorkingBuffer();
            ConvertBuffer(this.workingBuffer, this.xnaBuffer);
            this.instance.SubmitBuffer(this.xnaBuffer);
        }*/
    }
}