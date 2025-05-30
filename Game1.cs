﻿using System;
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
using System.Collections.Generic;
using Composer.UI;
using Composer.UI.Controls;

namespace Composer
{
    public class Game1 : Game
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
        private UIManager ui;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        /// <summary>
        /// Method for initializing the game
        /// </summary>
        protected override void Initialize()
        {

            // Setup graphics resolution

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            this.ui = new UIManager(this);

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


        /// <summary>
        /// Loads the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService<SpriteBatch>(spriteBatch);

            this.ui.DefaultFontSprite = new FontSprite(Texture2D.FromFile(this.graphics.GraphicsDevice, "Content\\MinimalSprite.png"), 5, 7, 10, 10);

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

            greenPanel.AddElement(new DebugPanel(this.ui)
                                    .Position(20, 30)
                                    .Size(100, 50)
                                    .Color(Color.Yellow));

            this.ui.Slider()
                .Position(10, 10)
                .Size(100, 50);
            

            // TODO: use this.Content to load your game content here
        }


        /// <summary>
        /// Main update method for the game
        /// </summary>
        /// <param name="gameTime"></param>
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

            // Update the UI

            this.ui.Update(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// Main draw method for the game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {

            // Draw the UI

            this.ui.Clear(Color.Black);
            this.ui.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}