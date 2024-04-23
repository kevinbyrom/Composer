using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Composer.Output;
using System.IO;

using Composer.Oscillators;

namespace Composer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int SampleRate = 44100;
        private const int SamplesPerBuffer = 44100;
        private DynamicSoundEffectInstance instance;
        private Synth synth;
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


            // Setup the output

            this.instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
            this.instance.Play();

            var xnaOutput = new BufferedXnaOutput(instance);
            this.output = new MixedOutput(xnaOutput);


            // Setup the synth

            synth = new Synth(SampleRate, this.output);
            //synth.SetupKey(0, Notes.C4);
            //synth.SetupKey(1, Notes.E4);
            //synth.SetupKey(2, Notes.G4);


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

            // Update the synth and outputs

            this.synth.Update(gameTime);
            this.output.Flush();


            // Check for new notes that were pressed

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { this.synth.KeyOn(0); } else { this.synth.KeyOff(0); }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { this.synth.KeyOn(1); } else { this.synth.KeyOff(1); }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { this.synth.KeyOn(2); } else { this.synth.KeyOff(2); }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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