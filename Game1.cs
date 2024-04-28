using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Composer.Output;
using System.IO;

using Composer.Oscillators;
using System.Text;

namespace Composer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int ScreenWidth = 1000;
        private const int ScreenHeight = 1000;
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
        private SignalTracker tracker;

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

            synth = new Synth(SampleRate, this.output, false);
            //synth.SetupKey(0, Notes.C4);
            //synth.SetupKey(1, Notes.E4);
            //synth.SetupKey(2, Notes.G4);


            // Setup the background buffer and font

            this.background = new Texture2D(GraphicsDevice, ScreenWidth, ScreenHeight);
            //this.font = Content.Load<SpriteFont>("Arial");

            if (debugMode)
                this.debugFile = new StreamWriter("d://temp//output.txt", true);

            tracker = new SignalTracker(ScreenWidth);

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
                this.tracker.Add(this.synth.LastSignal);
            }

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
            GraphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();

            //var builder = new StringBuilder();
            //builder.AppendJoin(" - ", this.synth.LastSignals);

            //var text = String.Format("TEST");
            //this.spriteBatch.DrawString(this.font, builder.ToString(), new Vector2(0, 0), Color.White);

            // Set the texture data

            var colors = new Color[ScreenWidth * ScreenHeight];

            var trackerData = this.tracker.GetAll();

            for (int x = 0; x < ScreenWidth; x++)
            {
                int ylen = (int)(trackerData[x].Value * (ScreenHeight / 2));

                colors[x + ((ScreenHeight / 2) * ScreenWidth)] = Color.White;

                for (int i = 0; i < Math.Abs(ylen); i++)
                {
                    int ydelta = ylen > 0 ? i : -i;
                    colors[(ScreenWidth - 1 - x) + (((ScreenHeight / 2) + ydelta) * ScreenWidth)] = Color.White;
                }
            }

            this.background.SetData(colors);

            this.spriteBatch.Draw(this.background, new Vector2(0, 0), Color.White);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}