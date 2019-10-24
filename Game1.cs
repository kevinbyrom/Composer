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
        private VoiceGroup voices;
        private Synth synth;
        private ISampleTarget output;
        private double time = 0;

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

            int numSamples = (int)(gameTime.ElapsedGameTime.TotalSeconds * SampleRate);

            for (int i = 0; i < numSamples; i++)
            {
                voices.Update(TimePerFrame);
                this.output.Write(voices.Samples);
                this.time += TimePerFrame;
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