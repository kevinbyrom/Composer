using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Composer.Output;
using System.IO;
using MonoGame.Extended;
using Composer.Oscillators;
using System.Text;

namespace Composer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int ScreenWidth = 500;
        private const int ScreenHeight = 500;
        private const int HalfScreenHeight = ScreenHeight / 2;
        private const int SampleRate = 44100;
        private const int SamplesPerBuffer = 44100;
        private DynamicSoundEffectInstance instance;
        private Synth synth;
        private ISignalTarget output;
        private bool debugMode = false;
        private StreamWriter debugFile;
        private Texture2D background;
        private SpriteFont font;
        private double timePerTick = 1.0 / (double)SampleRate;
        private double currTime = 0.0;
        private SignalBuffer recentSignals;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // Setup graphics resolution

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            // Setup the output

            this.instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
            this.instance.Play();

            var xnaOutput = new BufferedXnaOutput(instance);
            this.output = new MixedOutput(xnaOutput);


            // Setup the synth

            this.synth = new Synth(SampleRate, this.output, false);


            // Setup the background buffer and font

            this.background = new Texture2D(GraphicsDevice, ScreenWidth, ScreenHeight);
            //this.font = Content.Load<SpriteFont>("Arial");

            if (debugMode)
                this.debugFile = new StreamWriter("d://temp//output.txt", true);

            this.recentSignals = new SignalBuffer(ScreenWidth);

            base.Initialize();            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            // Determine how many ticks have elapsed since last time

            int ticks = (int)(gameTime.ElapsedGameTime.TotalSeconds * SampleRate);
            //int ticks = 1;

            // Update the synth and outputs

            for (int s = 0; s < ticks; s++)
            {
                this.synth.Update(currTime);
                this.currTime += this.timePerTick;
                this.recentSignals.Add(this.synth.LastSignal);
            }

            this.output.Flush();

            // Check for new notes that were pressed

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                this.synth.RootOctave = 5;             
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                this.synth.RootOctave = 3;
            else
                this.synth.RootOctave = 4;

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { this.synth.KeyOn(0); } else { this.synth.KeyOff(0); }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { this.synth.KeyOn(1); } else { this.synth.KeyOff(1); }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { this.synth.KeyOn(2); } else { this.synth.KeyOff(2); }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();

            var signals = this.recentSignals.GetAll();

            this.spriteBatch.DrawLine(0, HalfScreenHeight, ScreenWidth, HalfScreenHeight, Color.White);

            for (int x = 0; x < ScreenWidth; x++)
            {
                int ylen = (int)(signals[x].Value * (ScreenHeight / 2));

                this.spriteBatch.DrawLine(x, HalfScreenHeight, x, HalfScreenHeight - ylen, Color.White);
            }

            this.spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}