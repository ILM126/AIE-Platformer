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
    class PlayerClass
    {
        public bool PlayerFacingRight;

        public PlayerTrebleSketch TrebleSketch;

        public void PlayerMovement(GameTime gameTime)
        {
            //    if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    {
            //        TrebleSketch.Acceleration = 0.05f;
            //    }
            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //{
            //    TrebleSketch.Velocity += new Vector2(0, -2f);
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.A) && PlayerFacingRight == false)
            {
                TrebleSketch.Velocity = new Vector2(-2.5f, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && PlayerFacingRight == true)
            {
                TrebleSketch.Velocity = new Vector2(2.5f, 0);
            } else {
                TrebleSketch.Velocity = new Vector2(0, 0);
            }

            TrebleSketch.Position += TrebleSketch.Velocity;
        }

        public PlayerClass()
        {
            TrebleSketch = new PlayerTrebleSketch();
        }
    }
}
