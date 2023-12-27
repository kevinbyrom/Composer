using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Composer.Output;
using System.Diagnostics;

using Composer.Oscillators;
using Composer.Effects;
using System.IO;

namespace Composer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int SampleRate = 44100;
        private const double TimePerFrame = (double)1 / SampleRate;
        private const int SamplesPerBuffer = 44100;
        private DynamicSoundEffectInstance instance;
        private SampleTime time;
        private Voice[] voices;
        private ISignalSource source;
        private ISignalTarget output;
        private bool debugMode = false;
        private StreamWriter debugFile;


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


            // Setup the voices and mixer

            voices = new Voice[3];
            voices[0] = new Voice(new SineWaveOscillator(Notes.C4));
            voices[1] = new Voice(new SineWaveOscillator(Notes.E4));
            voices[2] = new Voice(new SineWaveOscillator(Notes.G4));

            var mixer = new Mixer();
            mixer.Sources.Add(voices[0]);
            mixer.Sources.Add(voices[1]);
            mixer.Sources.Add(voices[2]);
            this.source = mixer;

            // Setup the output

            this.instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
            this.instance.Play();

            var xnaOutput = new BufferedXnaOutput(instance);
            this.output = new MixedOutput(xnaOutput);

            this.time = new SampleTime(SampleRate);

            if (debugMode) 
                this.debugFile = new StreamWriter("d://temp//output.txt", true);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Signal signal = Signal.Zero;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            // Get the number of samples which have elapsed since last update

            int numSamples = (int)(gameTime.ElapsedGameTime.TotalSeconds * this.time.Rate);


            // Write the samples to the buffer and flush when done

            for (int i = 0; i < numSamples; i++)
            {                
                this.time.Current += TimePerFrame;
                this.time.Elapsed = TimePerFrame;

                signal = this.source.GetValue(this.time.Current);

                this.output.Write(this.time, signal);

                if (debugMode)
                    if (voices[0].CurrState == VoiceState.Playing || voices[1].CurrState == VoiceState.Playing)
                        debugFile.WriteLine(signal.ToString());
            }

            this.output.Flush();


            // Check for new notes that were pressed

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { voices[0].On(); } else { voices[0].Off(); }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { voices[1].On(); } else { voices[1].Off(); }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { voices[2].On(); } else { voices[2].Off(); }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private float NoteToFrequency(int note)
        {
            return (float)(440.0 * Math.Pow(2, (note - 9) / 12.0f));
        }
    }
}