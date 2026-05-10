using Composer.Output;
using Composer.UI;
using Composer.UI.Controls;
using Composer.Waves;
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGameGum;
using Monotaur;
using Monotaur.Graphics;
using Monotaur.Systems;
using Monotaur.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Composer.Scenes;


namespace Composer
{

    public class SynthGame : MonotaurGame
    {
        GumService GumUI => GumService.Default;

        private const int ScreenWidth = 1100;
        private const int ScreenHeight = 500;
        private const int HalfScreenHeight = ScreenHeight / 2;
        private const int SampleRate = 44100;
        private const int SamplesPerBuffer = 44100;


        public SynthGame() : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();


            // Change screen resolution

            //var graphics = new GraphicsDeviceManager(this);

            //graphics.PreferredBackBufferWidth = ScreenWidth;
            //graphics.PreferredBackBufferHeight = ScreenHeight;
            //graphics.ApplyChanges();


            // Setup the scenes

            this.Scenes.RegisterScene("default", new MainScene(this));
            this.Scenes.SetCurrentScene("default");
        }
    }
}