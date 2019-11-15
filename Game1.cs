using System;
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
        private const double TimePerFrame = (double)1 / SampleRate;
        private const int SamplesPerBuffer = 44100;
        private DynamicSoundEffectInstance instance;
        private Synth synth;
        private ISampleTarget output;
        private SampleTime time;

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

            this.instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
            this.instance.Play();

            var xnaOutput = new BufferedXnaOutput(instance);
            this.output = new MixedOutput(xnaOutput);


            // Setup the synth

            this.synth = new Synth(this.output);
            this.time = new SampleTime(SampleRate);

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

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { this.synth.PlayNote(0); } else { this.synth.NoteOff(0); }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { this.synth.PlayNote(2); } else { this.synth.NoteOff(2); }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { this.synth.PlayNote(4); } else { this.synth.NoteOff(4); }
            if (Keyboard.GetState().IsKeyDown(Keys.F)) { this.synth.PlayNote(5); } else { this.synth.NoteOff(5); }
            if (Keyboard.GetState().IsKeyDown(Keys.G)) { this.synth.PlayNote(7); } else { this.synth.NoteOff(7); }

            int numSamples = (int)(gameTime.ElapsedGameTime.TotalSeconds * this.time.Rate);

            for (int i = 0; i < numSamples; i++)
            {
                this.time.Current += TimePerFrame;
                this.time.Elapsed = TimePerFrame;

                this.synth.Update(this.time);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}