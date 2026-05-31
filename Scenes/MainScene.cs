using Composer;
using Composer.Output;
using Composer.UI.Controls;
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Monotaur;
using Monotaur.Components;
using Monotaur.Graphics;
using Monotaur.Models;
using Monotaur.Systems;
using Monotaur.UI;
using System;
using System.IO;


namespace Composer.Scenes
{
    public class MainScene : SceneBase
    {
        private const int ScreenWidth = 1100;
        private const int ScreenHeight = 500;
        private const int HalfScreenHeight = ScreenHeight / 2;
        private const int SampleRate = 44100;
        private const int SamplesPerBuffer = 44100;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
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
        private IUIManager ui;
        private IProjection _projection = new StandardProjection();
        private FontSprite fontSprite;


        public MainScene(Game game) : base(game)
        {
        }


        public override void Init()
        {

            // Setup the output

            try
            {
                this.instance = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Stereo);
                this.instance.Play();
                var xnaOutput = new BufferedXnaOutput(instance);
                this.output = new MixedOutput(xnaOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio initialization failed: {ex.Message}. Running without audio.");
                // Fallback to a dummy output or no output
                this.output = new DummyOutput(); // Assuming you have or can create a dummy output
            }


            // Setup the synth

            this.synth = new Synth(SampleRate, this.output, false);


            // Setup the background buffer and font

            this.fontSprite = new FontSprite(Texture2D.FromFile(this.Game.GraphicsDevice, "Content/MinimalSprite.png"), 5, 7, 10, 10);
            this.background = new Texture2D(this.Game.GraphicsDevice, ScreenWidth, ScreenHeight);
            //this.font = Content.Load<SpriteFont>("Arial");

            if (debugMode)
                this.debugFile = new StreamWriter("output.txt", true);

            this.recentSignals = new SignalBuffer(ScreenWidth);

            this.ui = (this.Game as MonotaurGame).UI;

            SetupControls();
        }


        public override void Enter()
        {
        }


        public override void Update(GameTime gameTime)
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
                this.Game.Exit();

            // Update the UI

            this.ui.Update(gameTime);
        }


        public override void Draw(IRenderer renderer, GameTime gameTime)
        {
            renderer.Projection = _projection;
            renderer.Clear(Color.CornflowerBlue);

            renderer.DefaultFontSprite = this.fontSprite;

            //this.ui.Clear(Color.Black);
            //this.ui.Draw(renderer, gameTime);
        }


        private void SetupControls()
        {
            this.ui.WaveView()
                .SetSignalBuffer(this.recentSignals)
                .Position(0, 0)
                .Size(ScreenWidth, ScreenHeight)
                .Color(Color.BlueViolet);


            this.ui.DebugPanel()
                .Position(0, 0)
                .Size(100, 20)
                .Color(Color.Red);

            this.ui.DebugPanel()
                .Position(10, 10)
                .Size(100, 20)
                .Color(Color.Blue);

            var greenPanel = this.ui.DebugPanel()
                .Position(20, 20)
                .Size(200, 100)
                .Color(Color.Green);

            greenPanel.AddElement(new DebugPanel(this.ui, null)
                                    .Position(20, 30)
                                    .Size(100, 50)
                                    .Color(Color.Yellow));

            this.ui.Slider()
                .Position(10, 10)
                .Size(100, 50);
                .Color(Color.Blue);
        }
    }
}
