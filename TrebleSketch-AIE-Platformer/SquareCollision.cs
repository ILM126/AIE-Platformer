using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace TrebleSketch_AIE_Platformer
{
    class SquareCollision
    {
        public Vector2 min;
        public Vector2 max;
        public SquareCollision(Vector2 position
        , Vector2 size)
        {
            Vector2 toEdge = size / 2;
            min.X = position.X - toEdge.X;
            min.Y = position.Y - toEdge.Y;
            max.X = position.X + toEdge.X;
            max.Y = position.Y + toEdge.Y;
        }

        static public bool CollsionCheck(SquareCollision pObject1, SquareCollision pObject2)
        {
            // reference: https://www.youtube.com/watch?v=ghqD3e37R7E
            return !(pObject1.max.X < pObject2.min.X || pObject2.max.X < pObject1.min.X
                || pObject1.max.Y < pObject2.min.Y || pObject2.max.Y < pObject1.min.Y);
        }

        public bool CollsionCheck(SquareCollision other)
        {
            return SquareCollision.CollsionCheck(this, other);
        }
    }
}
