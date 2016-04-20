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
        public SquareCollision BoxCollision;

        public Vector2 MousePosition;
        public Vector2 MouseSize;
        public int MouseScale;
        public MouseState state;

        public Texture2D MouseTexture;
        public Texture2D MouseTexturePressed;

        public Rectangle MenuButton;

        int i = 0;

        public void Initialize()
        {
            MouseSize = new Vector2(MouseTexture.Width, MouseTexture.Height);
            MouseScale = 1;
        }

        public void Update()
        {
            state = Mouse.GetState();
            MousePosition = new Vector2(state.X, state.Y);

            if (state.LeftButton == ButtonState.Pressed)
            {
                // Do cool stuff here
                // Console.WriteLine(++;
                Console.WriteLine("[INFO] Mouse: I am being left pressed for " + ++i + " frames!");
            }

            // Check if the mouse position is inside the rectangle
            //if (area.Contains(mousePosition))
            //{
            //    backgroundTexture = hoverTexture;
            //}
            //else
            //{
            //    backgroundTexture = defaultTexture;
            //}

            //var destination = new Rectangle(100, 1000, 50, 50);
            //bool mouseOver = destination.Contains(state.X, state.Y);

            /*
            Rectangle area = someRectangle;

            // Check if the mouse position is inside the rectangle
            if (area.Contains(mousePosition))
            {
                backgroundTexture = hoverTexture;
            }
            else
            {
                backgroundTexture = defaultTexture;
            }

            Console.WriteLine("[INFO] mouseOver = " + mouseOver.ToString());*/

            // UpdateBounds();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(
                    MouseTexturePressed
                    , MousePosition
                    , new Rectangle(
                        0
                        , 0
                        , MouseTexture.Width
                        , MouseTexture.Height)
                    , Color.White);
            } else if (state.LeftButton == ButtonState.Released)
            {
                spriteBatch.Draw(
                    MouseTexture
                    , MousePosition
                    , new Rectangle(
                        0
                        , 0
                        , MouseTexture.Width
                        , MouseTexture.Height)
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
