using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Composer.UI
{
    public interface IView : IViewContainer
    {
        IView Parent { get; }

        Vector2 Pos { get; set; }

        Vector2 ScreenPos { get; }

        Point Size { get; set; }

        Color Color { get; set; }

        RenderTarget2D RenderTarget { get; }

        void Update(GameTime time);

        void Draw(GameTime time, SpriteBatch spriteBatch);

        bool ProcessInput(InputState inputState);

        void UpdateScreenPos();
    }

    public interface IViewContainer
    {
        void AddView(IView view);
    }

    public static class IViewExtensions
    {

        public static IView SetPosition(this IView view, int x, int y)
        {
            view.Pos = new Vector2(x, y);
            return view;
        }

        public static IView SetSize(this IView view, int width, int height)
        {
            view.Size = new Point(width, height);
            return view;
        }

        public static IView SetColor(this IView view, Color color)
        {
            view.Color = color;
            return view;
        }
    }
}
