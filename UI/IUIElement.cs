using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Composer.UI
{
    public interface IUIElement : IUIElementContainer
    {
        IUIElement Parent { get; set; }

        Point Pos { get; set; }

        Point Size { get; set; }

        int Width { get; }

        int Height { get; }

        Point ScreenPos { get; set; }

        Rectangle ScreenRect { get; }

        Color Color { get; set; } 

        RenderTarget2D RenderTarget { get; }

        void Update(GameTime time);

        void Draw(GameTime time, SpriteBatch spriteBatch);

        void UpdateScreenPos();

        void OnMouseEnter(MouseState state);

        void OnMouseMove(MouseState state);

        void OnMouseExit(MouseState state);

    }

    public interface IUIElementContainer
    {
        IEnumerable<IUIElement> Elements { get; }
        void AddElement(IUIElement element);
    }

    public static class IUIElementExtensions
    {

        public static IUIElement SetPosition(this IUIElement element, int x, int y)
        {
            element.Pos = new Point(x, y);
            return element;
        }

        public static IUIElement SetSize(this IUIElement element, int width, int height)
        {
            element.Size = new Point(width, height);
            return element;
        }

        public static IUIElement SetColor(this IUIElement element, Color color)
        {
            element.Color = color;
            return element;
        }

        public static Point ScreenToRelativePos(this IUIElement element, Point screenPos)
        {
            return element.ScreenPos - screenPos;
        }

        public static Point RelativeToScreenPos(this IUIElement element, Point pos)
        {
            return element.ScreenPos + pos;
        }

        public static bool IsInScreenBounds(this IUIElement element, Point screenPos)
        {
            return element.ScreenRect.Contains(screenPos);
        }


        public static IUIElement FindElementAtScreenPos(this IUIElement element, Point screenPos)
        {
            IUIElement target = null;

            if (element.ScreenRect.Contains(screenPos))
                target = element;

            foreach (var child in element.Elements)
            {
                var subTarget = child.FindElementAtScreenPos(screenPos);

                if (subTarget != null)
                    target = subTarget;
            }
            return target;
        }

        public static IUIElement FindElementAtScreenPos(this List<IUIElement> elements, Point screenPos)
        {
            IUIElement target = null;

            foreach (var element in elements)
            {
                if (element.ScreenRect.Contains(screenPos))
                    target = element;

                foreach (var child in element.Elements)
                {
                    var subTarget = child.FindElementAtScreenPos(screenPos);

                    if (subTarget != null)
                        target = subTarget;
                }
            }
            
            return target;
        }
    }
}
