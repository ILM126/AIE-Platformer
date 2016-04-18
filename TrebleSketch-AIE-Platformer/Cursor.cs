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
    class Cursor
    {
        public Vector2 MousePosition;
        public MouseState state;
        public Texture2D MouseTexture;

        public void Update()
        {
            state = Mouse.GetState();
            MousePosition = new Vector2(state.X, state.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                MouseTexture
                , MousePosition
                , new Rectangle(0, 0, MouseTexture.Width, MouseTexture.Height)
                , Color.White);
        }
    }
}
