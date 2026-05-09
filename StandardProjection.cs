using System;
using Microsoft.Xna.Framework;
using Monotaur.Graphics;

namespace Composer;

public class StandardProjection : IProjection
{
    public Vector2 WorldToScreen(Vector3 worldPos)
    {
        return new Vector2(worldPos.X, worldPos.Y);
    }
}
