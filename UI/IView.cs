using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Composer.UI
{
    public interface IView : IViewContainer
    {
        IView Parent { get; }

        Point Pos { get; set; }

        Point Size { get; set; }

        int Width { get; }

        int Height { get; }

        Point ScreenPos { get; set; }

        Rectangle ScreenRect { get; }

        Color Color { get; set; }

        IEnumerable<IView> SubViews { get; }   

        RenderTarget2D RenderTarget { get; }

        void Update(GameTime time);

        void Draw(GameTime time, SpriteBatch spriteBatch);

        void UpdateScreenPos();

        void OnMouseEnter(MouseState state);

        void OnMouseMove(MouseState state);

        void OnMouseExit(MouseState state);

    }

    public interface IViewContainer
    {
        void AddView(IView view);
    }

    public static class IViewExtensions
    {

        public static IView SetPosition(this IView view, int x, int y)
        {
            view.Pos = new Point(x, y);
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

        public static Point ScreenToRelativePos(this IView view, Point screenPos)
        {
            return view.ScreenPos - screenPos;
        }

        public static Point RelativeToScreenPos(this IView view, Point pos)
        {
            return view.ScreenPos + pos;
        }

        public static bool IsInScreenBounds(this IView view, Point screenPos)
        {
            return view.ScreenRect.Contains(screenPos);
        }


        public static IView FindViewAtScreenPos(this IView view, Point screenPos)
        {
            IView target = null;

            if (view.ScreenRect.Contains(screenPos))
                target = view;

            foreach (var subView in view.SubViews)
            {
                var subTarget = subView.FindViewAtScreenPos(screenPos);

                if (subTarget != null)
                    target = subTarget;
            }
            return target;
        }

        public static IView FindViewAtScreenPos(this List<IView> views, Point screenPos)
        {
            IView target = null;

            foreach (var view in views)
            {
                if (view.ScreenRect.Contains(screenPos))
                    target = view;

                foreach (var subView in view.SubViews)
                {
                    var subTarget = subView.FindViewAtScreenPos(screenPos);

                    if (subTarget != null)
                        target = subTarget;
                }
            }
            
            return target;
        }
    }
}
