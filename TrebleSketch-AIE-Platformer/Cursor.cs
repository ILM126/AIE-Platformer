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
using EclipsingGameUtils;

namespace TrebleSketch_AIE_Platformer
{
    class Cursor
    {
        public enum MenuButtonState
        {
            Render,
            Hover,
            Clicking
        }

        public SquareCollision BoxCollision;

        public Vector2 MousePosition;
        public Vector2 MouseSize;
        public int MouseScale;
        public MouseState state;

        public Texture2D MouseTexture;
        public Texture2D MouseTexturePressed;

        public Rectangle CursorRect;

        public void Initialize()
        {
            MouseSize = new Vector2(MouseTexture.Width, MouseTexture.Height);
            MouseScale = 1;
        }

        public void Update()
        {
            state = Mouse.GetState();
            MousePosition = new Vector2(state.X, state.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CursorRect = new Rectangle(
                        0
                        , 0
                        , MouseTexture.Width
                        , MouseTexture.Height);

            if (state.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(
                    MouseTexturePressed
                    , MousePosition
                    , CursorRect
                    , Color.White);
            } else if (state.LeftButton == ButtonState.Released)
            {
                spriteBatch.Draw(
                    MouseTexture
                    , MousePosition
                    , CursorRect
                    , Color.White);
            }
        }

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            BoxCollision = new SquareCollision(MousePosition, MouseSize * MouseScale);
        }

        protected bool SquareCollisionCheck(SceneObjects pOther)
        {
            return BoxCollision.CollsionCheck(pOther.BoxCollision);
        }

        public bool CollisionCheck(SceneObjects other)
        {
            bool mouseCollision = SquareCollisionCheck(other);
            if (mouseCollision)
            {
                return true;
            }
            return false;
        }
    }
}
